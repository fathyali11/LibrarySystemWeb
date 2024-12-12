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
    private readonly string _filesPath = webHostEnvironment.WebRootPath;
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

        var bookPath=await SaveFile(request.Document, $"{_filesPath}/books");
        var imagePath = await SaveFile(request.Image, $"{_filesPath}/images");
        book.FilePath = $"https://localhost:7157//books/{bookPath}";
        book.ImagePath = $"https://localhost:7157//images/{imagePath}";

        var result = await _unitOfWork.BookRepository.AddAsync(book, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        var response = _mapper.Map<BookResponse>(result);
        return response;
    }
    public async Task<OneOf<IEnumerable<BookResponse>, Error>> GetAllBooksAsync( bool? includeNotAvailable = null,CancellationToken cancellationToken = default)
    {
        IEnumerable<Book> books;

        if (includeNotAvailable == true)
            books = await _unitOfWork.BookRepository.GetAllAsync(cancellationToken: cancellationToken);
        else
        {
            books = await _unitOfWork.BookRepository.GetAllAsync(
                predicate: x => x.IsActive,
                cancellationToken: cancellationToken);
        }

        if (!books.Any())
            return BookErrors.NotFound;

        var response = _mapper.Map<List<BookResponse>>(books);
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
        var response = _mapper.Map<BookResponse>(bookFromDb);
        return response;
    }

    private async Task<string> SaveFile(IFormFile file,string path)
    {
        var randomfileName = $"{Path.GetRandomFileName()}{Path.GetExtension(file.FileName)}";
        var fullPath= Path.Combine(path, randomfileName);
        using var stream = File.Create(fullPath);
        await file.CopyToAsync(stream);

        return randomfileName;
    }
}
