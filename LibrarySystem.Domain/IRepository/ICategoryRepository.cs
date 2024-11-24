using LibrarySystem.Domain.DTO.Categories;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.IRepository;
public interface ICategoryRepository:IGenericRepository<Category>
{
    Task<Category?> UpdateAsync(int id,CategoryRequest request);
}
