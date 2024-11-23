using AutoMapper;
using LibrarySystem.Data.Data;
using LibrarySystem.Domain.DTO.Categories;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;

namespace LibrarySystem.Data.Repository;
public class CategoryRepository(ApplicationDbContext context,IMapper mapper) : GenericRepository<Category>(context), ICategoryRepository
{
    private readonly IMapper _mapper=mapper;
    public async Task<Category> UpdateAsync(int id, CategoryRequest request)
    {
        var category = await GetByIdAsync(id);
        _mapper.Map(request, category);
        return category;
    }
}
