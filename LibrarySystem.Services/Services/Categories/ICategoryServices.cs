namespace LibrarySystem.Services.Services.Categories;
public interface ICategoryServices:ICategoryRepository
{
    Task<OneOf<IEnumerable<CategoryResponse>,Error>> GetAllCategoriesAsync(CancellationToken cancellationToken=default);
    Task<OneOf<IEnumerable<CategoryWithBooksResponse>,Error>> GetAllCategoriesWithBooksAsync(CancellationToken cancellationToken=default);
    Task<OneOf<CategoryResponse, Error>> GetCategoryByIdAsync(int id,CancellationToken cancellationToken=default);
    Task<OneOf<CategoryResponse, Error>> AddCategoryAsync(CategoryRequest request, CancellationToken cancellationToken = default);
    Task<OneOf<CategoryResponse, Error>> UpdateCategoryAsync(int id,CategoryRequest request,CancellationToken cancellationToken=default);
    Task<OneOf<CategoryResponse, Error>> ToggelCategoryAsync(int id,CancellationToken cancellationToken=default);
}
