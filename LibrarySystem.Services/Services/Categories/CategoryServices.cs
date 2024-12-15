namespace LibrarySystem.Services.Services.Categories;
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
    public async Task<OneOf<IEnumerable<CategoryResponse>, Error>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
    {
        const string cacheKey = "All-Categories";
        var categories = await _hybridCache.GetOrCreateAsync(
                cacheKey,
                async cached =>
                {
                    var categoryEntities = await _unitOfWork.CategoryRepository.GetAllAsync(cancellationToken: cancellationToken);
                    return _mapper.Map<List<CategoryResponse>>(categoryEntities);
                }
            );

        return categories;
    }
    public async Task<OneOf<IEnumerable<CategoryWithBooksResponse>, Error>> GetAllCategoriesWithBooksAsync(CancellationToken cancellationToken = default)
    {
        const string cachKey = "All-Categories-Books";
        var categories = await _hybridCache.GetOrCreateAsync(
                cachKey,
                async cached =>
                {
                    var categoreisWithBooks = await _unitOfWork.CategoryRepository.GetAllAsync(includedNavigations: "Books", cancellationToken: cancellationToken);
                    return _mapper.Map<List<CategoryWithBooksResponse>>(categoreisWithBooks);
                }
            );
        return categories;
    }
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
