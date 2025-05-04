using LibrarySystem.Services.Services.BlobStorages;

namespace LibrarySystem.Services.Services.Books;
/// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="bookServices"]/BookServices'/>
public class BookServices(ApplicationDbContext context,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    HybridCache hybridCache,ILogger<BookServices> logger,
    IBlobStorageService blobStorageService) : BookRepository(context, mapper), IBookServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly HybridCache _hybridCache = hybridCache;
    private readonly IMapper _mapper=mapper;
    private readonly IBlobStorageService _blobStorageService = blobStorageService;



    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="bookServices"]/AddBookAsync'/>
    public async Task<OneOf<BookResponse, Error>> AddBookAsync(CreateBookRequest request, CancellationToken cancellationToken = default)
    {
        var bookIsExists=await _unitOfWork.BookRepository.IsExits(x=>x.Title== request.Document.FileName, cancellationToken);
        if (bookIsExists)
            return BookErrors.Found;

        var categoryIsExists=await _unitOfWork.CategoryRepository.IsExits(x=>x.Id==request.CategoryId, cancellationToken);
        if (!categoryIsExists)
            return CategoryErrors.NotFound;

        var authorIsExists = await _unitOfWork.AuthorRepository.IsExits(x => x.Id == request.AuthorId, cancellationToken);
        if (!authorIsExists)
            return AuthorErrors.NotFound;
        
        Book book = _mapper.Map<Book>(request);

        // var bookPath=await SaveFile(request.Document, _bookPath);
        var filePath  = await _blobStorageService.UploadFileAsync(request.Document.FileName, request.Document,"doc");
        var imagePath = await _blobStorageService.UploadFileAsync(request.Image.FileName, request.Image, "image");
        book.FilePath = filePath;
        book.ImagePath = imagePath;




        logger.LogInformation("file path {book.FilePath} \n image path {book.ImagePath}",book.FilePath,book.ImagePath);
        

        var result = await _unitOfWork.BookRepository.AddAsync(book, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllBooksCachedKey, cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllAvailableBooksCachedKey, cancellationToken);
        var response = _mapper.Map<BookResponse>(result);
        return response;
    }
    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="bookServices"]/GetAllBooksAsync'/>
    public async Task<OneOf<PaginatedResult<Book,BookResponse>, Error>> GetAllBooksAsync(PaginatedRequest request, bool? includeNotAvailable = null,CancellationToken cancellationToken = default)
    {
        IEnumerable<Book> books;

        if (includeNotAvailable == true)
            books = await _hybridCache.GetOrCreateAsync(GeneralConsts.AllBooksCachedKey,
                async bookEntity =>
                {
                    var query = _unitOfWork.BookRepository.GetAll(cancellationToken: cancellationToken);
                    var bookEntities = await query.ToListAsync();
                    return bookEntities;
                }
                 );
        else
            books = await _hybridCache.GetOrCreateAsync(GeneralConsts.AllAvailableBooksCachedKey,
                    async bookEntity =>
                    {
                        var query = _unitOfWork.BookRepository.GetAll(x => x.IsActive, cancellationToken: cancellationToken);
                        var bookEntities = await query.ToListAsync();
                        return bookEntities;
                    }
                     );

        //if we work with local azureit
        foreach (var bookEntity in books)
        {
            bookEntity.FilePath += $"?{_blobStorageService.GenerateSasToken(bookEntity.FilePath, "file")}";
            bookEntity.ImagePath += $"?{_blobStorageService.GenerateSasToken(bookEntity.ImagePath, "image")}";
        }
        if (!string.IsNullOrEmpty(request.SearchTerm))
            books = books.Where(x => x.Title.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase));

        if(!string.IsNullOrEmpty(request.SortTerm))
            books = books.AsQueryable().OrderBy($"{request.SortTerm} {request.SortBy}").ToList();

        var paginatedBooks=PaginatedResult<Book, BookResponse>.Create(books, request.PageNumber, request.PageSize);
        paginatedBooks.Result = _mapper.Map<List<BookResponse>>(paginatedBooks.Values);
        return paginatedBooks;
    }
    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="bookServices"]/GetBookByIdAsync'/>
    public async Task<OneOf<BookResponse, Error>> GetBookByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if(id<0)
            return BookErrors.NotFound;

        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if(book == null) 
            return BookErrors.NotFound;
        var response=_mapper.Map<BookResponse>(book);
        // if we work with local azureit
        response.FilePath += $"?{_blobStorageService.GenerateSasToken(book.FilePath, "file")}";
        response.ImagePath += $"?{_blobStorageService.GenerateSasToken(book.ImagePath, "image")}";
        return response;
    }
    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="bookServices"]/UpdateBookAsync'/>
    public async Task<OneOf<BookResponse, Error>> UpdateBookAsync(int id, UpdateBookRequest request, CancellationToken cancellationToken = default)
    {
        if(id < 0)
            return BookErrors.NotFound;

        var bookFromDb = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if( bookFromDb == null)
            return BookErrors.NotFound;

        bookFromDb=_mapper.Map(request, bookFromDb);
        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllBooksCachedKey, cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllAvailableBooksCachedKey, cancellationToken);
        var response=_mapper.Map<BookResponse>(bookFromDb);
        return response;
    }
    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="bookServices"]/ToggleBookAsync'/>
    public async Task<OneOf<BookResponse, Error>> ToggleBookAsync(int id, CancellationToken cancellationToken = default)
    {
        if(id < 0)
            return BookErrors.NotFound;
        var bookFromDb = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (bookFromDb == null)
            return BookErrors.NotFound;
        bookFromDb.IsActive=!bookFromDb.IsActive;
        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllBooksCachedKey, cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllAvailableBooksCachedKey, cancellationToken);
        var response = _mapper.Map<BookResponse>(bookFromDb);
        return response;
    }
    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="bookServices"]/UpdateBookFileAsync'/>
    public async Task<OneOf<BookResponse, Error>> UpdateBookFileAsync(int id, BookFileRequest request, CancellationToken cancellationToken = default)
    {
        if (id < 0)
            return BookErrors.NotFound;

        var bookFromDb = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (bookFromDb == null)
            return BookErrors.NotFound;
        var isRemoved =await _blobStorageService.DeleteFileAsync(bookFromDb.Title, "doc");
        if (!isRemoved)
            return BookErrors.NotFound;

        _mapper.Map(request, bookFromDb);
        bookFromDb.FilePath = await _blobStorageService.UploadFileAsync(bookFromDb.Title, request.Document, "doc");
        
        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllBooksCachedKey, cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllAvailableBooksCachedKey, cancellationToken);
        return _mapper.Map<BookResponse>(bookFromDb);
    }
    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="bookServices"]/UpdateBookImageAsync'/>
    public async Task<OneOf<BookResponse, Error>> UpdateBookImageAsync(int id, BookImageRequest request, CancellationToken cancellationToken = default)
    {
        if(id<0)
            return BookErrors.NotFound;

        var bookFromDb = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if(bookFromDb == null)
            return BookErrors.NotFound;
        var isRemoved =await _blobStorageService.DeleteFileAsync(bookFromDb.Title, "image");
        if (!isRemoved)
            return BookErrors.NotFound;

        _mapper.Map(request, bookFromDb);
        bookFromDb.ImagePath =await _blobStorageService.UploadFileAsync(request.Image.FileName, request.Image, "image");
        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllBooksCachedKey, cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllAvailableBooksCachedKey, cancellationToken);
        return _mapper.Map<BookResponse>(bookFromDb);
    }
}
