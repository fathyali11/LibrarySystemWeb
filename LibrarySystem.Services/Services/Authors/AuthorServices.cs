using AutoMapper;
using LibrarySystem.Data.Data;
using LibrarySystem.Data.Repository;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.Errors;
using LibrarySystem.Domain.DTO.Author;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Services.Services.Cashing;
using Microsoft.Extensions.Logging;
using OneOf;

namespace LibrarySystem.Services.Services.Authors;
public class AuthorServices(ApplicationDbContext context,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICacheServices cacheServices,
    ILogger<AuthorServices> logger) 
    : AuthorRepository(context, mapper), IAuthorServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly ICacheServices _cacheServices=cacheServices;
    private readonly ILogger<AuthorServices> _logger = logger;
    private readonly IMapper _mapper=mapper;
    public async Task<OneOf<IEnumerable<AuthorResponse>, Error>> GetAllAuthorsAsync(CancellationToken cancellationToken = default)
    {
        const string cashKey = "All-Authors";
        var cashedValue=await _cacheServices.GetAsync<List<AuthorResponse>>(cashKey,cancellationToken);
        if(cashedValue is not null )
        {
            _logger.LogInformation("data from cach center");
            return cashedValue;
        }

        var authors = await _unitOfWork.AuthorRepository.GetAllAsync(cancellationToken: cancellationToken);
        var response=_mapper.Map<List<AuthorResponse>>(authors);
        if (response is null)
            return AuthorErrors.NotFound;
        await _cacheServices.SetAsync(cashKey, response,cancellationToken);
        _logger.LogInformation("data from data base");
        return response;
    }
    public async Task<OneOf<IEnumerable<AuthorWithBooksResponse>, Error>> GetAllAuthorsWithBooksAsync(CancellationToken cancellationToken = default)
    {
        const string cashKey = "All-Authors-Books";
        var cashedValue = await _cacheServices.GetAsync<List<AuthorWithBooksResponse>>(cashKey, cancellationToken);
        if (cashedValue is not null)
        {
            _logger.LogInformation("data from cach center");
            return cashedValue;
        }


        var authors=await _unitOfWork.AuthorRepository.GetAllAsync(includedNavigations:"Books",cancellationToken:cancellationToken);
        var response=_mapper.Map<List<AuthorWithBooksResponse>>(authors);
        if(response is null)
            return AuthorErrors.NotFound;

        await _cacheServices.SetAsync(cashKey, response,cancellationToken);
        _logger.LogInformation("data from data base");
        return response;
    }
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
    public async Task<OneOf<AuthorResponse, Error>> AddAuthorAsync(AuthorRequest request, CancellationToken cancellationToken = default)
    {
        var author=_mapper.Map<Author>(request);
        var result=await _unitOfWork.AuthorRepository.AddAsync(author,cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        var response=_mapper.Map<AuthorResponse>(result);
        await _cacheServices.RemoveAsync("All-Authors", cancellationToken);
        await _cacheServices.RemoveAsync("All-Authors-Books", cancellationToken);
        return response is not null ? response : AuthorErrors.NotFound;
    }

    public async Task<OneOf<AuthorResponse, Error>> UpdateAuthorAsync(int id, AuthorRequest request, CancellationToken cancellationToken = default)
    {
        if(id<=0)
            return AuthorErrors.NotFound;

        var result = await _unitOfWork.AuthorRepository.UpdateAsync(id, request);
        if(result is null)
            return AuthorErrors.NotFound;

        await _unitOfWork.SaveChanges(cancellationToken);
        var response = _mapper.Map<AuthorResponse>(result);
        await _cacheServices.RemoveAsync("All-Authors", cancellationToken);
        await _cacheServices.RemoveAsync("All-Authors-Books", cancellationToken);
        return response is not null ? response : AuthorErrors.NotFound;
    }

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
        await _cacheServices.RemoveAsync("All-Authors", cancellationToken);
        await _cacheServices.RemoveAsync("All-Authors-Books", cancellationToken);
        return response is not null ? response : AuthorErrors.NotFound;
    }
}
