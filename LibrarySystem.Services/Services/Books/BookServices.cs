using AutoMapper;
using LibrarySystem.Data.Data;
using LibrarySystem.Data.Repository;
using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Services.Services.Books;
public class BookServices(ApplicationDbContext context, IMapper mapper,IUnitOfWork unitOfWork) : BookRepository(context, mapper), IBookServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly IMapper _mapper=mapper;
    public async Task<IEnumerable<BookResponse>> GetAllBooksAsync(CancellationToken cancellationToken = default)
    {
        var books = await _unitOfWork.BookRepository.GetAllAsync(cancellationToken: cancellationToken);
        var response=_mapper.Map<List<BookResponse>>(books);
        return response;
    }
    public async Task<BookResponse> AddBookAsync(BookRequest request, CancellationToken cancellationToken = default)
    {
        Book book=_mapper.Map<Book>(request);
        var result=await _unitOfWork.BookRepository.AddAsync(book, cancellationToken);
        var response=_mapper.Map<BookResponse>(result);
        return response;
    }
}
