using LibrarySystem.Domain.Abstractions.Pagination;
using LibrarySystem.Domain.DTO.Common;

namespace LibrarySystem.Services.Services.Books;
/// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="iBookServices"]/IBookServices'/>
public interface IBookServices : IBookRepository
{
    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="iBookServices"]/AddBookAsync'/>
    Task<OneOf<BookResponse, Error>> AddBookAsync(CreateBookRequest request, CancellationToken cancellationToken = default);

    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="iBookServices"]/GetAllBooksAsync'/>
    Task<OneOf<PaginatedResult<Book,BookResponse>, Error>> GetAllBooksAsync(PaginatedRequest request, bool? includeNotAvailable = null, CancellationToken cancellationToken = default);

    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="iBookServices"]/GetBookByIdAsync'/>
    Task<OneOf<BookResponse, Error>> GetBookByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="iBookServices"]/UpdateBookAsync'/>
    Task<OneOf<BookResponse, Error>> UpdateBookAsync(int id, UpdateBookRequest request, CancellationToken cancellationToken = default);

    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="iBookServices"]/ToggleBookAsync'/>
    Task<OneOf<BookResponse, Error>> ToggleBookAsync(int id, CancellationToken cancellationToken = default);

    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="iBookServices"]/UpdateBookFileAsync'/>
    Task<OneOf<BookResponse, Error>> UpdateBookFileAsync(int id, BookFileRequest request, CancellationToken cancellationToken = default);

    /// <include file='ExternalServicesDocs\BooksDocs.xml' path='/docs/members[@name="iBookServices"]/UpdateBookImageAsync'/>
    Task<OneOf<BookResponse, Error>> UpdateBookImageAsync(int id, BookImageRequest request, CancellationToken cancellationToken = default);
}
