using LibrarySystem.Domain.DTO.Author;
using LibrarySystem.Services.Services.Authors;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Domain.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using LibrarySystem.Services.CustomAuthorization;
using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using LibrarySystem.Domain.DTO.Common;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("token")]
public class AuthorsController(IAuthorServices authorServices) : ControllerBase
{
    private readonly IAuthorServices _authorServices = authorServices;
    [HttpPost("")]
    [HasPermission(ManagerPermissions.CreateAuthor)]
    public async Task<IActionResult> Add([FromBody] AuthorRequest request, CancellationToken cancellationToken)
    {
        var result = await _authorServices.AddAuthorAsync(request, cancellationToken);
        return result.Match<IActionResult>(
            response => CreatedAtAction(nameof(Get), new { id = response.Id }, response),
            error => error.ToProblem()
            );
    }

    [HttpGet("")]
    [HasPermission(SellerPermissions.GetAuthors)]
    public async Task<IActionResult> GetAll([FromQuery] PaginatedRequest request,CancellationToken cancellationToken)
    {
        var result = await _authorServices.GetAllAuthorsAsync(request,cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
    [HttpGet("with-books")]
    [HasPermission(MemberPermissions.GetAuthors)]
    public async Task<IActionResult> GetAllWithBooks([FromQuery] PaginatedRequest request, CancellationToken cancellationToken)
    {
        var result = await _authorServices.GetAllAuthorsWithBooksAsync(request,cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }

    [HttpGet("{id}")]
    [HasPermission(MemberPermissions.GetAuthors)]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _authorServices.GetAuthorAsync(id, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
    [HttpPut("{id}")]
    [HasPermission(ManagerPermissions.UpdateAuthor)]
    public async Task<IActionResult> Update(int id, AuthorRequest request, CancellationToken cancellationToken)
    {
        var result = await _authorServices.UpdateAuthorAsync(id, request, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }

    [HttpPut("toggel-status-{id}")]
    [HasPermission(ManagerPermissions.UpdateAuthor)]
    public async Task<IActionResult> AddToggel(int id, CancellationToken cancellationToken)
    {
        var result = await _authorServices.ToggelAuthorAsync(id, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
}
