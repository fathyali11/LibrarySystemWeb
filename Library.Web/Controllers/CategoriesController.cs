using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using LibrarySystem.Domain.DTO.Categories;
using LibrarySystem.Domain.DTO.Common;
using LibrarySystem.Services.CustomAuthorization;
using LibrarySystem.Services.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("token")]
public class CategoriesController(ICategoryServices categoryServices) : ControllerBase
{
    private readonly ICategoryServices _categoryServices=categoryServices;
    [HttpPost("")]
    [HasPermission(ManagerPermissions.CreateCategory)]
    public async Task<IActionResult> Add([FromBody] CategoryRequest request,CancellationToken cancellationToken)
    {
        var result=await _categoryServices.AddCategoryAsync(request,cancellationToken);
        return result.Match<IActionResult>(
            response => CreatedAtAction(nameof(Get), new {id=response.Id},response),
            error => error.ToProblem()
            );
    }

    [HttpGet("")]
    [HasPermission(MemberPermissions.GetCategories)]
    public async Task<IActionResult> GetAll([FromQuery] PaginatedRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryServices.GetAllCategoriesAsync(request, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
    [HttpGet("with-books")]
    [HasPermission(MemberPermissions.GetCategories)]
    public async Task<IActionResult> GetAllWithBooks([FromQuery] PaginatedRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryServices.GetAllCategoriesWithBooksAsync(request, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }

    [HttpGet("{id}")]
    [HasPermission(MemberPermissions.GetCategories)]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _categoryServices.GetCategoryByIdAsync(id, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
    [HttpPut("{id}")]
    [HasPermission(ManagerPermissions.UpdateCategory)]
    public async Task<IActionResult> Update(int id,CategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryServices.UpdateCategoryAsync(id,request, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }

    [HttpPut("toggel-status-{id}")]
    [HasPermission(ManagerPermissions.UpdateCategory)]
    public async Task<IActionResult> AddToggel(int id, CancellationToken cancellationToken)
    {
        var result = await _categoryServices.ToggelCategoryAsync(id,cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
}
