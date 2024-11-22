

using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.IRepository;
using OneOf;

namespace LibrarySystem.Services.Services.Books;
public interface IBookServices:IBookRepository
{
    Task<OneOf<BookResponse,Error>> AddBookAsync(BookRequest request,CancellationToken cancellationToken=default);
    Task<OneOf<IEnumerable<BookResponse>,Error>> GetAllBooksAsync(CancellationToken cancellationToken=default);
    Task<OneOf<BookResponse, Error>> GetBookByIdAsync(int id,CancellationToken cancellationToken=default);
    Task<OneOf<BookResponse,Error>> UpdateBookAsync(int id,BookRequest request,CancellationToken cancellationToken=default);
}
