using AutoMapper;
using LibrarySystem.Data.Data;
using LibrarySystem.Data.Repository;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.Errors;
using LibrarySystem.Domain.DTO.Author;
using LibrarySystem.Domain.Entities;
using OneOf;

namespace LibrarySystem.Services.Services.Authors;
public class AuthorServices(ApplicationDbContext context, IMapper mapper,IUnitOfWork unitOfWork) : AuthorRepository(context, mapper), IAuthorServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly IMapper _mapper=mapper;
    public async Task<OneOf<IEnumerable<AuthorResponse>, Error>> GetAllAuthorsAsync(CancellationToken cancellationToken = default)
    {
        var authors = await _unitOfWork.AuthorRepository.GetAllAsync(cancellationToken: cancellationToken);
        var response=_mapper.Map<List<AuthorResponse>>(authors);
        return response is not null?response:AuthorErrors.NotFound;
    }
    public async Task<OneOf<IEnumerable<AuthorWithBooksResponse>, Error>> GetAllAuthorsWithBooksAsync(CancellationToken cancellationToken = default)
    {
        var authors=await _unitOfWork.AuthorRepository.GetAllAsync(includedNavigations:"Books",cancellationToken:cancellationToken);
        var response=_mapper.Map<List<AuthorWithBooksResponse>>(authors);
        return response is not null ? response : AuthorErrors.NotFound;
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
        return response is not null ? response : AuthorErrors.NotFound;
    }
}
