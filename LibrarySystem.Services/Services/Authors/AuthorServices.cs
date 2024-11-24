using AutoMapper;
using LibrarySystem.Data.Data;
using LibrarySystem.Data.Repository;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.Errors;
using LibrarySystem.Domain.DTO.Author;
using LibrarySystem.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using OneOf;
using System.Text.Json;

namespace LibrarySystem.Services.Services.Authors;
public class AuthorServices(ApplicationDbContext context,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IDistributedCache distributedCache,
    ILogger<AuthorServices> logger) 
    : AuthorRepository(context, mapper), IAuthorServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly IDistributedCache _distributedCache = distributedCache;
    private readonly ILogger<AuthorServices> _logger = logger;
    private readonly IMapper _mapper=mapper;
    public async Task<OneOf<IEnumerable<AuthorResponse>, Error>> GetAllAuthorsAsync(CancellationToken cancellationToken = default)
    {
        const string cashKey = "All-Authors";
        var cashedValue=await _distributedCache.GetStringAsync(cashKey,cancellationToken);
        if(cashedValue is not null )
        {
            _logger.LogInformation("data from cach center");
            return JsonSerializer.Deserialize<List<AuthorResponse>>(cashedValue)!;
        }

        var authors = await _unitOfWork.AuthorRepository.GetAllAsync(cancellationToken: cancellationToken);
        var response=_mapper.Map<List<AuthorResponse>>(authors);
        if (response is null)
            return AuthorErrors.NotFound;
        var serializedData = JsonSerializer.Serialize(response);
        await _distributedCache.SetStringAsync(cashKey, serializedData,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            },
            cancellationToken);
        _logger.LogInformation("data from data base");
        return response;
    }
    public async Task<OneOf<IEnumerable<AuthorWithBooksResponse>, Error>> GetAllAuthorsWithBooksAsync(CancellationToken cancellationToken = default)
    {
        const string cashKey = "All-Authors-Books";
        var cashedValue = await _distributedCache.GetStringAsync(cashKey, cancellationToken);
        if (cashedValue is not null)
        {
            _logger.LogInformation("data from cach center");
            return JsonSerializer.Deserialize<List<AuthorWithBooksResponse>>(cashedValue)!;
        }


        var authors=await _unitOfWork.AuthorRepository.GetAllAsync(includedNavigations:"Books",cancellationToken:cancellationToken);
        var response=_mapper.Map<List<AuthorWithBooksResponse>>(authors);
        if(response is null)
            return AuthorErrors.NotFound;

        var serializedData = JsonSerializer.Serialize(response);
        await _distributedCache.SetStringAsync(cashKey, serializedData,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            },
            cancellationToken);
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
        await _distributedCache.RemoveAsync("All-Authors", cancellationToken);
        await _distributedCache.RemoveAsync("All-Authors-Books", cancellationToken);
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
        await _distributedCache.RemoveAsync("All-Authors", cancellationToken);
        await _distributedCache.RemoveAsync("All-Authors-Books", cancellationToken);
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
        await _distributedCache.RemoveAsync("All-Authors", cancellationToken);
        await _distributedCache.RemoveAsync("All-Authors-Books", cancellationToken);
        return response is not null ? response : AuthorErrors.NotFound;
    }
}
