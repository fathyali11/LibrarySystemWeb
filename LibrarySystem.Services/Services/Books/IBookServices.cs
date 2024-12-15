namespace LibrarySystem.Services.Services.Books;
public interface IBookServices:IBookRepository
{
    Task<OneOf<BookResponse,Error>> AddBookAsync(CreateBookRequest request,CancellationToken cancellationToken=default);
    Task<OneOf<IEnumerable<BookResponse>,Error>> GetAllBooksAsync(bool? includeNotAvailable = null, CancellationToken cancellationToken=default);
    Task<OneOf<BookResponse, Error>> GetBookByIdAsync(int id,CancellationToken cancellationToken=default);
    Task<OneOf<BookResponse,Error>> UpdateBookAsync(int id, UpdateBookRequest request,CancellationToken cancellationToken=default);
    Task<OneOf<BookResponse,Error>> ToggleBookAsync(int id,CancellationToken cancellationToken=default);
    Task<OneOf<BookResponse, Error>> UpdateBookFileAsync(int id, BookFileRequest request, CancellationToken cancellationToken = default);
    Task<OneOf<BookResponse, Error>> UpdateBookImageAsync(int id, BookImageRequest request, CancellationToken cancellationToken = default);



}
