using LibrarySystem.Domain.DTO.Author;
using LibrarySystem.Domain.Entities;
namespace LibrarySystem.Domain.IRepository;
public interface IAuthorRepository:IGenericRepository<Author>
{
    Task<Author?> UpdateAsync(int id,AuthorRequest request);
}
