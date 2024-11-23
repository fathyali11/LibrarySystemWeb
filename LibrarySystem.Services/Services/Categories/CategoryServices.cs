using AutoMapper;
using LibrarySystem.Data.Data;
using LibrarySystem.Data.Repository;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.Errors;
using LibrarySystem.Domain.DTO.Categories;
using LibrarySystem.Domain.Entities;
using OneOf;

namespace LibrarySystem.Services.Services.Categories;
public class CategoryServices(ApplicationDbContext context, IMapper mapper,IUnitOfWork unitOfWork)
    : CategoryRepository(context, mapper), ICategoryServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly IMapper _mapper=mapper;
    public async Task<OneOf<IEnumerable<CategoryResponse>, Error>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _unitOfWork.CategoryRepository.GetAllAsync(cancellationToken:cancellationToken);
        var responses =_mapper.Map<List<CategoryResponse>>(categories);
        return responses is not null?responses:CategoryErrors.NotFound;
    }
    public async Task<OneOf<IEnumerable<CategoryWithBooksResponse>, Error>> GetAllCategoriesWithBooksAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _unitOfWork.CategoryRepository.GetAllAsync(includedNavigations:"Books",cancellationToken: cancellationToken);
        var responses = _mapper.Map<List<CategoryWithBooksResponse>>(categories);
        return responses is not null ? responses : CategoryErrors.NotFound;
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
        return response is not null ? response : CategoryErrors.NotFound;
    }


}
