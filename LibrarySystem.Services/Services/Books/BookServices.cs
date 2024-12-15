using AutoMapper;
using LibrarySystem.Data.Data;
using LibrarySystem.Data.Repository;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.Errors;
using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Hybrid;
using OneOf;

namespace LibrarySystem.Services.Services.Books;
public class BookServices(ApplicationDbContext context,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    HybridCache hybridCache,
    IWebHostEnvironment webHostEnvironment) : BookRepository(context, mapper), IBookServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly HybridCache _hybridCache = hybridCache;
    private readonly IMapper _mapper=mapper;
    private readonly string _bookPath = $"{webHostEnvironment.WebRootPath}\\books";
    private readonly string _imagePath = $"{webHostEnvironment.WebRootPath}\\images";
    private const string cachedKeyAllBooks= "all-books";
    private const string cachedKeyAllAvailableBooks= "all-available-books";
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

        var bookPath=await SaveFile(request.Document, _bookPath);
        var imagePath = await SaveFile(request.Image, _imagePath);
        book.FilePath = $"https://localhost:7157//books/{bookPath}";
        book.ImagePath = $"https://localhost:7157//images/{imagePath}";
        book.RandomTitle= bookPath;
        book.RandomImageName=imagePath;

        var result = await _unitOfWork.BookRepository.AddAsync(book, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync(cachedKeyAllBooks, cancellationToken);
        await _hybridCache.RemoveAsync(cachedKeyAllAvailableBooks, cancellationToken);
        var response = _mapper.Map<BookResponse>(result);
        return response;
    }
    public async Task<OneOf<IEnumerable<BookResponse>, Error>> GetAllBooksAsync( bool? includeNotAvailable = null,CancellationToken cancellationToken = default)
    {
        IEnumerable<Book> cached;

        if (includeNotAvailable == true)
            cached = await _hybridCache.GetOrCreateAsync(cachedKeyAllBooks,
                async bookEntities =>
                     await _unitOfWork.BookRepository.GetAllAsync(cancellationToken: cancellationToken)
                 );
        else
            cached = await _hybridCache.GetOrCreateAsync(cachedKeyAllAvailableBooks,
                    async bookEntities =>
                         await _unitOfWork.BookRepository.GetAllAsync(x => x.IsAvailable && x.IsActive, cancellationToken: cancellationToken)
                     );


        if (!cached.Any())
            return BookErrors.NotFound;

        var response = _mapper.Map<List<BookResponse>>(cached);
        return response;
    }
    public async Task<OneOf<BookResponse, Error>> GetBookByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if(id<0)
            return BookErrors.NotFound;

        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if(book == null) 
            return BookErrors.NotFound;
        var response=_mapper.Map<BookResponse>(book);
        return response;
    }
    public async Task<OneOf<BookResponse, Error>> UpdateBookAsync(int id, UpdateBookRequest request, CancellationToken cancellationToken = default)
    {
        if(id < 0)
            return BookErrors.NotFound;

        var bookFromDb = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if( bookFromDb == null)
            return BookErrors.NotFound;

        bookFromDb=_mapper.Map(request, bookFromDb);
        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync(cachedKeyAllBooks, cancellationToken);
        await _hybridCache.RemoveAsync(cachedKeyAllAvailableBooks, cancellationToken);
        var response=_mapper.Map<BookResponse>(bookFromDb);
        return response;
    }
    public async Task<OneOf<BookResponse, Error>> ToggleBookAsync(int id, CancellationToken cancellationToken = default)
    {
        if(id < 0)
            return BookErrors.NotFound;
        var bookFromDb = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (bookFromDb == null)
            return BookErrors.NotFound;
        bookFromDb.IsActive=!bookFromDb.IsActive;
        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync(cachedKeyAllBooks, cancellationToken);
        await _hybridCache.RemoveAsync(cachedKeyAllAvailableBooks, cancellationToken);
        var response = _mapper.Map<BookResponse>(bookFromDb);
        return response;
    }
    public async Task<OneOf<BookResponse, Error>> UpdateBookFileAsync(int id, BookFileRequest request, CancellationToken cancellationToken = default)
    {
        if (id < 0)
            return BookErrors.NotFound;

        var bookFromDb = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (bookFromDb == null)
            return BookErrors.NotFound;
        var fullPath = $"{_bookPath}\\{bookFromDb.RandomTitle}";
        var isRemoved = await RemoveFile(fullPath);
        if (!isRemoved)
            return BookErrors.NotFound;

        _mapper.Map(request, bookFromDb);
        var documentPath = await SaveFile(request.Document, _bookPath);
        bookFromDb.FilePath = $"https://localhost:7157//books/{documentPath}";
        bookFromDb.RandomTitle = documentPath;
        await _unitOfWork.SaveChanges(cancellationToken);
        return _mapper.Map<BookResponse>(bookFromDb);
    }
    public async Task<OneOf<BookResponse, Error>> UpdateBookImageAsync(int id, BookImageRequest request, CancellationToken cancellationToken = default)
    {
        if(id<0)
            return BookErrors.NotFound;

        var bookFromDb = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if(bookFromDb == null)
            return BookErrors.NotFound;
        var fullPath = $"{_imagePath}\\{bookFromDb.RandomImageName}";
        var isRemoved =await RemoveFile(fullPath);
        if(!isRemoved)
            return BookErrors.NotFound;

        _mapper.Map(request, bookFromDb);
        var imagePath = await SaveFile(request.Image, _imagePath);
        bookFromDb.ImagePath = $"https://localhost:7157//images/{imagePath}";
        bookFromDb.RandomImageName=imagePath;
        await _unitOfWork.SaveChanges(cancellationToken);
        return _mapper.Map<BookResponse>(bookFromDb);
    }


    private async Task<string> SaveFile(IFormFile file,string path)
    {
        var randomfileName = $"{Path.GetRandomFileName()}{Path.GetExtension(file.FileName)}";
        var fullPath= Path.Combine(path, randomfileName);
        using var stream = File.Create(fullPath);
        await file.CopyToAsync(stream);

        return randomfileName;
    }

    private async Task<bool> RemoveFile(string path)
    {
        try
        {
            if(File.Exists(path))
            {
                await Task.Run(() => File.Delete(path)); 
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
}
