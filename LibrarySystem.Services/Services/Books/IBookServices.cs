

using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.IRepository;

namespace LibrarySystem.Services.Services.Books;
public interface IBookServices:IBookRepository
{
    Task<BookResponse> AddBookAsync(BookRequest request,CancellationToken cancellationToken=default);
    Task<IEnumerable<BookResponse>> GetAllBooksAsync(CancellationToken cancellationToken=default);
}
