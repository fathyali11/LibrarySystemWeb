using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.DTO.Categories;
using LibrarySystem.Services.Services.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriessController(ICategoryServices categoryServices) : ControllerBase
{
    private readonly ICategoryServices _categoryServices=categoryServices;
    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] CategoryRequest request,CancellationToken cancellationToken)
    {
        var result=await _categoryServices.AddCategoryAsync(request,cancellationToken);
        return result.Match<IActionResult>(
            response => CreatedAtAction(nameof(Get), new {id=response.Id},response),
            error => error.ToProblem()
            );
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _categoryServices.GetAllCategoriesAsync( cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
    [HttpGet("with-books")]
    public async Task<IActionResult> GetAllWithBooks(CancellationToken cancellationToken)
    {
        var result = await _categoryServices.GetAllCategoriesWithBooksAsync(cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _categoryServices.GetCategoryByIdAsync(id, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,CategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryServices.UpdateCategoryAsync(id,request, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }

    [HttpPut("toggel-status-{id}")]
    public async Task<IActionResult> AddToggel(int id, CancellationToken cancellationToken)
    {
        var result = await _categoryServices.ToggelCategoryAsync(id,cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
}
