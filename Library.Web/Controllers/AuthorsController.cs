using LibrarySystem.Domain.DTO.Author;
using LibrarySystem.Services.Services.Authors;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Domain.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthorsController(IAuthorServices authorServices) : ControllerBase
{
    private readonly IAuthorServices _authorServices = authorServices;
    [HttpPost("")]
    [EndpointDescription("Add new author")]
    [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AuthorRequest request, CancellationToken cancellationToken)
    {
        var result = await _authorServices.AddAuthorAsync(request, cancellationToken);
        return result.Match<IActionResult>(
            response => CreatedAtAction(nameof(Get), new { id = response.Id }, response),
            error => error.ToProblem()
            );
    }

    [HttpGet("")]
    [EndpointDescription("Get all authors")]
    [ProducesResponseType(typeof(IEnumerable<AuthorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _authorServices.GetAllAuthorsAsync(cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
    [HttpGet("with-books")]
    [EndpointDescription("Get all authors with books")]
    [ProducesResponseType(typeof(IEnumerable<AuthorWithBooksResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllWithBooks(CancellationToken cancellationToken)
    {
        var result = await _authorServices.GetAllAuthorsWithBooksAsync(cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }

    [HttpGet("{id}")]
    [EndpointDescription("Get author by id")]
    [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _authorServices.GetAuthorAsync(id, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
    [HttpPut("{id}")]
    [EndpointDescription("Update author")]
    [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, AuthorRequest request, CancellationToken cancellationToken)
    {
        var result = await _authorServices.UpdateAuthorAsync(id, request, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }

    [HttpPut("toggel-status-{id}")]
    [EndpointDescription("Toggel author status")]
    [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddToggel(int id, CancellationToken cancellationToken)
    {
        var result = await _authorServices.ToggelAuthorAsync(id, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
}
