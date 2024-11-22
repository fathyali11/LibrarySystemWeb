using AutoMapper;
using LibrarySystem.Data.Data;
using LibrarySystem.Data.Repository;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.Errors;
using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.Entities;
using OneOf;

namespace LibrarySystem.Services.Services.Books;
public class BookServices(ApplicationDbContext context, IMapper mapper,IUnitOfWork unitOfWork) : BookRepository(context, mapper), IBookServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly IMapper _mapper=mapper;

    public async Task<OneOf<BookResponse, Error>> AddBookAsync(BookRequest request, CancellationToken cancellationToken = default)
    {
        var bookIsExists=await _unitOfWork.BookRepository.IsExits(x=>x.Title== request.Title, cancellationToken);
        if (bookIsExists)
            return BookErrors.Found;
        //ToDo check Author is found and check category also is found or not

        Book book = _mapper.Map<Book>(request);
        var result = await _unitOfWork.BookRepository.AddAsync(book, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        var response = _mapper.Map<BookResponse>(result);
        return response;
    }
    public async Task<OneOf<IEnumerable<BookResponse>, Error>> GetAllBooksAsync(CancellationToken cancellationToken = default)
    {
        var books = await _unitOfWork.BookRepository.GetAllAsync(cancellationToken: cancellationToken);
        var response=_mapper.Map<List<BookResponse>>(books);
        return response;
    }
    public async Task<OneOf<BookResponse, Error>> GetBookByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
        var response=_mapper.Map<BookResponse>(book);
        return response;
    }
    public async Task<OneOf<BookResponse, Error>> UpdateBookAsync(int id, BookRequest request, CancellationToken cancellationToken = default)
    {

        var bookFromDb = await _unitOfWork.BookRepository.GetByIdAsync(id);

        bookFromDb=_mapper.Map(request, bookFromDb);
        await _unitOfWork.SaveChanges(cancellationToken);
        var response=_mapper.Map<BookResponse>(bookFromDb);
        return response;
    }
    
}
