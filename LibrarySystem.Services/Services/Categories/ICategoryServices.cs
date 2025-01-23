using LibrarySystem.Domain.Abstractions.Pagination;
using LibrarySystem.Domain.DTO.Common;

namespace LibrarySystem.Services.Services.Categories;
/// <include file='ExternalServicesDocs\CategoriesDocs.xml' path='/docs/members[@name="iCategoryServices"]/ICategoryServices'/>
public interface ICategoryServices:ICategoryRepository
{
    /// <include file='ExternalServicesDocs\CategoriesDocs.xml' path='/docs/members[@name="iCategoryServices"]/GetAllCategoriesAsync'/>
    Task<OneOf<PaginatedResult<Category, CategoryResponse>,Error>> GetAllCategoriesAsync(PaginatedRequest request, CancellationToken cancellationToken=default);
    /// <include file='ExternalServicesDocs\CategoriesDocs.xml' path='/docs/members[@name="iCategoryServices"]/GetAllCategoriesWithBooksAsync'/>
    Task<OneOf<PaginatedResult<Category, CategoryWithBooksResponse>,Error>> GetAllCategoriesWithBooksAsync(PaginatedRequest request, CancellationToken cancellationToken=default);
    /// <include file='ExternalServicesDocs\CategoriesDocs.xml' path='/docs/members[@name="iCategoryServices"]/GetCategoryByIdAsync'/>
    Task<OneOf<CategoryResponse, Error>> GetCategoryByIdAsync(int id,CancellationToken cancellationToken=default);
    /// <include file='ExternalServicesDocs\CategoriesDocs.xml' path='/docs/members[@name="iCategoryServices"]/AddCategoryAsync'/>
    Task<OneOf<CategoryResponse, Error>> AddCategoryAsync(CategoryRequest request, CancellationToken cancellationToken = default);
    /// <include file='ExternalServicesDocs\CategoriesDocs.xml' path='/docs/members[@name="iCategoryServices"]/UpdateCategoryAsync'/>
    Task<OneOf<CategoryResponse, Error>> UpdateCategoryAsync(int id,CategoryRequest request,CancellationToken cancellationToken=default);
    /// <include file='ExternalServicesDocs\CategoriesDocs.xml' path='/docs/members[@name="iCategoryServices"]/ToggelCategoryAsync'/>
    Task<OneOf<CategoryResponse, Error>> ToggelCategoryAsync(int id,CancellationToken cancellationToken=default);
}
