using LibrarySystem.Domain.Abstractions.Pagination;
using LibrarySystem.Domain.DTO.Common;

namespace LibrarySystem.Services.Services.Authors;
/// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="iAuthorServices"]/IAuthorServices'/>

public interface IAuthorServices:IAuthorRepository
{
    /// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="iAuthorServices"]/GetAllAuthorsAsync'/>
    Task<OneOf<PaginatedResult<Author, AuthorResponse>, Error>> GetAllAuthorsAsync(PaginatedRequest request, CancellationToken cancellationToken=default);
    /// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="iAuthorServices"]/GetAllAuthorsWithBooksAsync'/>
    Task<OneOf<PaginatedResult<Author, AuthorWithBooksResponse>, Error>> GetAllAuthorsWithBooksAsync(PaginatedRequest request, CancellationToken cancellationToken = default);
    /// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="iAuthorServices"]/GetAuthorAsync'/>
    Task<OneOf<AuthorResponse, Error>> GetAuthorAsync(int id,CancellationToken cancellationToken=default);

    /// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="iAuthorServices"]/AddAuthorAsync'/>
    Task<OneOf<AuthorResponse, Error>> AddAuthorAsync(AuthorRequest request, CancellationToken cancellationToken = default);
    /// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="iAuthorServices"]/UpdateAuthorAsync'/>
    Task<OneOf<AuthorResponse, Error>> UpdateAuthorAsync(int id,AuthorRequest request, CancellationToken cancellationToken = default);
    /// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="iAuthorServices"]/ToggelAuthorAsync'/>
    Task<OneOf<AuthorResponse, Error>> ToggelAuthorAsync(int id, CancellationToken cancellationToken = default);
}
