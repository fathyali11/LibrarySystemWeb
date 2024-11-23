using AutoMapper;
using LibrarySystem.Data.Data;
using LibrarySystem.Domain.DTO.Author;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;

namespace LibrarySystem.Data.Repository;
public class AuthorRepository(ApplicationDbContext context,IMapper mapper) : GenericRepository<Author>(context), IAuthorRepository
{
    private readonly IMapper _mapper=mapper;
    public async Task<Author> UpdateAsync(int id, AuthorRequest request)
    {
        Author ?author=await GetByIdAsync(id);
        _mapper.Map(request,author);
        return author!;
    }
}
