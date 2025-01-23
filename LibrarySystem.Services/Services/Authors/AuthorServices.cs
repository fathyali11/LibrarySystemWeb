using LibrarySystem.Domain.Abstractions.Pagination;
using LibrarySystem.Domain.DTO.Common;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace LibrarySystem.Services.Services.Authors;
/// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="authorServices"]/AuthorServices'/>

public class AuthorServices(ApplicationDbContext context,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    HybridCache hybridCache,
    ILogger<AuthorServices> logger) 
    : AuthorRepository(context, mapper), IAuthorServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly HybridCache _hybridCache=hybridCache;
    private readonly ILogger<AuthorServices> _logger = logger;
    private readonly IMapper _mapper=mapper;
    /// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="authorServices"]/GetAllAuthorsAsync'/>
    public async Task<OneOf<PaginatedResult<Author,AuthorResponse>, Error>> GetAllAuthorsAsync(PaginatedRequest request,CancellationToken cancellationToken = default)
    {
        string cashKey = $"All-Authors";
        var authers = await _hybridCache.GetOrCreateAsync(
                cashKey,
                async cached =>
                {
                    var query = _unitOfWork.AuthorRepository.GetAll(cancellationToken: cancellationToken);
                    var authorEntities =await query.ToListAsync(cancellationToken);
                    return authorEntities;
                }
            );

        if (!string.IsNullOrEmpty(request.SearchTerm))
            authers = authers.Where(x => x.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

        if (!string.IsNullOrEmpty(request.SortTerm))
            authers = authers.AsQueryable()
                         .OrderBy($"{request.SortTerm} {request.SortBy}")
                         .ToList();
        var paginatedAuthors = PaginatedResult<Author, AuthorResponse>.Create(authers, request.PageNumber, request.PageSize);

        paginatedAuthors.Result = _mapper.Map<List<AuthorResponse>>(paginatedAuthors.Values);
        return paginatedAuthors;
    }
    /// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="authorServices"]/GetAllAuthorsWithBooksAsync'/>
    public async Task<OneOf<PaginatedResult<Author,AuthorWithBooksResponse>, Error>> GetAllAuthorsWithBooksAsync(PaginatedRequest request, CancellationToken cancellationToken = default)
    {
        
        const string cashKey = "All-Authors-Books";
        var authers = await _hybridCache.GetOrCreateAsync(
                cashKey,
                async cached =>
                {
                    var query =_unitOfWork.AuthorRepository.GetAll(includedNavigations: "Books", cancellationToken: cancellationToken);
                    var authorEntities = await query.ToListAsync();
                    return authorEntities;
                }
            );

        if (!string.IsNullOrEmpty(request.SearchTerm))
            authers = authers.Where(x => x.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

        if (!string.IsNullOrEmpty(request.SortTerm))
            authers = authers.AsQueryable()
                         .OrderBy($"{request.SortTerm} {request.SortBy}")
                         .ToList();

        var paginatedAuthors = PaginatedResult<Author, AuthorWithBooksResponse>.Create(authers, request.PageNumber, request.PageSize);

        paginatedAuthors.Result = _mapper.Map<List<AuthorWithBooksResponse>>(paginatedAuthors.Values);
        return paginatedAuthors;

        
    }
    /// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="authorServices"]/GetAuthorAsync'/>
    public async Task<OneOf<AuthorResponse, Error>> GetAuthorAsync(int id, CancellationToken cancellationToken = default)
    {
        if(id<=0)
            return AuthorErrors.NotFound;

        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
        if(author is null) 
            return AuthorErrors.NotFound;

        var response = _mapper.Map<AuthorResponse>(author);
        return response is not null ? response : AuthorErrors.NotFound;
    }
    /// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="authorServices"]/AddAuthorAsync'/>
    public async Task<OneOf<AuthorResponse, Error>> AddAuthorAsync(AuthorRequest request, CancellationToken cancellationToken = default)
    {
        var author=_mapper.Map<Author>(request);
        var result=await _unitOfWork.AuthorRepository.AddAsync(author,cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        var response=_mapper.Map<AuthorResponse>(result);
        await _hybridCache.RemoveAsync("All-Authors", cancellationToken);
        await _hybridCache.RemoveAsync("All-Authors-Books", cancellationToken);
        return response is not null ? response : AuthorErrors.NotFound;
    }
    /// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="authorServices"]/UpdateAuthorAsync'/>
    public async Task<OneOf<AuthorResponse, Error>> UpdateAuthorAsync(int id, AuthorRequest request, CancellationToken cancellationToken = default)
    {
        if(id<=0)
            return AuthorErrors.NotFound;

        var result = await _unitOfWork.AuthorRepository.UpdateAsync(id, request);
        if(result is null)
            return AuthorErrors.NotFound;

        await _unitOfWork.SaveChanges(cancellationToken);
        var response = _mapper.Map<AuthorResponse>(result);
        await _hybridCache.RemoveAsync("All-Authors", cancellationToken);
        await _hybridCache.RemoveAsync("All-Authors-Books", cancellationToken);
        return response is not null ? response : AuthorErrors.NotFound;
    }
    /// <include file='ExternalServicesDocs\AuthorsDocs.xml' path='/docs/members[@name="authorServices"]/ToggelAuthorAsync'/>
    public async Task<OneOf<AuthorResponse, Error>> ToggelAuthorAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return AuthorErrors.NotFound;

        var authorFromDb = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
        if (authorFromDb is null)
            return AuthorErrors.NotFound;

        authorFromDb!.IsDeleted = !authorFromDb.IsDeleted;
        await _unitOfWork.SaveChanges(cancellationToken: cancellationToken);
        var response = _mapper.Map<AuthorResponse>(authorFromDb);
        await _hybridCache.RemoveAsync("All-Authors", cancellationToken);
        await _hybridCache.RemoveAsync("All-Authors-Books", cancellationToken);
        return response is not null ? response : AuthorErrors.NotFound;
    }
}
