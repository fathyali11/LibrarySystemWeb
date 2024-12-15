namespace LibrarySystem.Services.Services.Authors;
public interface IAuthorServices:IAuthorRepository
{
    Task<OneOf<IEnumerable<AuthorResponse>,Error>> GetAllAuthorsAsync(CancellationToken cancellationToken=default);
    Task<OneOf<IEnumerable<AuthorWithBooksResponse>,Error>> GetAllAuthorsWithBooksAsync(CancellationToken cancellationToken=default);
    Task<OneOf<AuthorResponse, Error>> GetAuthorAsync(int id,CancellationToken cancellationToken=default);

    Task<OneOf<AuthorResponse, Error>> AddAuthorAsync(AuthorRequest request, CancellationToken cancellationToken = default);
    Task<OneOf<AuthorResponse, Error>> UpdateAuthorAsync(int id,AuthorRequest request, CancellationToken cancellationToken = default);

    Task<OneOf<AuthorResponse, Error>> ToggelAuthorAsync(int id, CancellationToken cancellationToken = default);
}
