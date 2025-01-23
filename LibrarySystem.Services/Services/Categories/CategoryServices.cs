using LibrarySystem.Domain.Abstractions.Pagination;
using LibrarySystem.Domain.DTO.Common;
using System.Linq.Dynamic.Core;

namespace LibrarySystem.Services.Services.Categories;
/// <include file='ExternalServicesDocs\CategoriesDocs.xml' path='/docs/members[@name="categoryServices"]/CategoryServices'/>
public class CategoryServices(ApplicationDbContext context, 
    IMapper mapper,IUnitOfWork unitOfWork,
    HybridCache hybridCache,
    ILogger<CategoryServices> logger)
    : CategoryRepository(context, mapper), ICategoryServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly HybridCache _hybridCache = hybridCache;
    private readonly ILogger<CategoryServices> _logger = logger;
    private readonly IMapper _mapper=mapper;
    public async Task<OneOf<PaginatedResult<Category,CategoryResponse>, Error>> GetAllCategoriesAsync(PaginatedRequest request,CancellationToken cancellationToken = default)
    {
        const string cacheKey = "All-Categories";
        var categories = await _hybridCache.GetOrCreateAsync(
                cacheKey,
                async cached =>
                {
                    var query = _unitOfWork.CategoryRepository.GetAll(cancellationToken: cancellationToken);
                    var categoryEntities = await query.ToListAsync();
                    return categoryEntities;
                }
            );
        if (!string.IsNullOrEmpty(request.SearchTerm))
            categories = categories.Where(x => x.Name.Contains(request.SearchTerm)).ToList();

        if (!string.IsNullOrEmpty(request.SortTerm))
            categories = categories.AsQueryable()
                         .OrderBy($"{request.SortTerm} {request.SortBy}")
                         .ToList();

        var paginatedCategories = PaginatedResult<Category, CategoryResponse>.Create(categories, request.PageNumber, request.PageSize);
        paginatedCategories.Result = _mapper.Map<List<CategoryResponse>>(paginatedCategories.Values);
        return paginatedCategories;
    }
    /// <include file='ExternalServicesDocs\CategoriesDocs.xml' path='/docs/members[@name="categoryServices"]/GetAllCategoriesWithBooksAsync'/>
    public async Task<OneOf<PaginatedResult<Category,CategoryWithBooksResponse>, Error>> GetAllCategoriesWithBooksAsync(PaginatedRequest request,CancellationToken cancellationToken = default)
    {
        const string cachKey = "All-Categories-Books";
        var categories = await _hybridCache.GetOrCreateAsync(
                cachKey,
                async cached =>
                {
                    var query  = _unitOfWork.CategoryRepository.GetAll(includedNavigations: "Books", cancellationToken: cancellationToken);
                    var categoreisWithBooks = await query.ToListAsync();
                    return categoreisWithBooks
                }
            );
        if (!string.IsNullOrEmpty(request.SearchTerm))
            categories = categories.Where(x => x.Name.Contains(request.SearchTerm)).ToList();

        if (!string.IsNullOrEmpty(request.SortTerm))
            categories = categories.AsQueryable()
                         .OrderBy($"{request.SortTerm} {request.SortBy}")
                         .ToList();

        var paginatedCategories = PaginatedResult<Category, CategoryWithBooksResponse>.Create(categories, request.PageNumber, request.PageSize);

        paginatedCategories.Result = _mapper.Map<List<CategoryWithBooksResponse>>(paginatedCategories.Values);
        return paginatedCategories;
    }
    /// <include file='ExternalServicesDocs\CategoriesDocs.xml' path='/docs/members[@name="categoryServices"]/GetCategoryByIdAsync'/>
    public async Task<OneOf<CategoryResponse, Error>> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 0)
            return CategoryErrors.NotFound;

        var category=await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        if(category == null)
            return CategoryErrors.NotFound;

        var response=_mapper.Map<CategoryResponse>(category);
        return response is not null ? response : CategoryErrors.NotFound;
    }
    /// <include file='ExternalServicesDocs\CategoriesDocs.xml' path='/docs/members[@name="categoryServices"]/AddCategoryAsync'/>
    public async Task<OneOf<CategoryResponse, Error>> AddCategoryAsync(CategoryRequest request, CancellationToken cancellationToken = default)
    {
        var category=_mapper.Map<Category>(request);
        var result=await _unitOfWork.CategoryRepository.AddAsync(category);
        await _unitOfWork.SaveChanges(cancellationToken);
        var response=_mapper.Map<CategoryResponse>(result);
        await _hybridCache.RemoveAsync("All-Categories", cancellationToken);
        await _hybridCache.RemoveAsync("All-Categories-Books", cancellationToken);
        return response is not null ? response : CategoryErrors.NotFound;
    }
    /// <include file='ExternalServicesDocs\CategoriesDocs.xml' path='/docs/members[@name="categoryServices"]/UpdateCategoryAsync'/>
    public async Task<OneOf<CategoryResponse, Error>> UpdateCategoryAsync(int id, CategoryRequest request, CancellationToken cancellationToken = default)
    {
        if(id < 0)
            return CategoryErrors.NotFound;
        var categoryIsExists=await _unitOfWork.CategoryRepository.IsExits(x=>x.Name==request.Name);
        if(categoryIsExists)
            return CategoryErrors.Found;

        var result = await _unitOfWork.CategoryRepository.UpdateAsync(id, request);
        await _unitOfWork.SaveChanges(cancellationToken);
        var response = _mapper.Map<CategoryResponse>(result);
        await _hybridCache.RemoveAsync("All-Categories", cancellationToken);
        await _hybridCache.RemoveAsync("All-Categories-Books", cancellationToken);
        return response is not null ? response : CategoryErrors.NotFound;
    }
    /// <include file='ExternalServicesDocs\CategoriesDocs.xml' path='/docs/members[@name="categoryServices"]/ToggelCategoryAsync'/>
    public async Task<OneOf<CategoryResponse, Error>> ToggelCategoryAsync(int id, CancellationToken cancellationToken = default)
    {
        if(id < 0)
            return CategoryErrors.NotFound;

        var categoryFromDb=await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        categoryFromDb!.IsDeleted=!categoryFromDb.IsDeleted;
        await _unitOfWork.SaveChanges(cancellationToken: cancellationToken);
        var response=_mapper.Map<CategoryResponse>(categoryFromDb);
        await _hybridCache.RemoveAsync("All-Categories", cancellationToken);
        await _hybridCache.RemoveAsync("All-Categories-Books", cancellationToken);
        return response is not null ? response : CategoryErrors.NotFound;
    }
}
