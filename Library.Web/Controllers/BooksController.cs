using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Services.CustomAuthorization;
using LibrarySystem.Services.Services.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]

public class BooksController(IBookServices bookServices) : ControllerBase
{
    private readonly IBookServices _bookServices=bookServices;
    
    [HttpGet("include-{include}")]
    [HasPermission(MemberPermissions.GetBooks)]
    [EndpointDescription("Get all books")]
    [ProducesResponseType(typeof(IEnumerable<BookResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll([FromRoute]bool include,CancellationToken cancellationToken)
    {
        
        var result=await _bookServices.GetAllBooksAsync(include,cancellationToken:cancellationToken);
        return result.Match<IActionResult>(
            res => Ok(res),
            error=>error.ToProblem());
    }
    [HttpPost("")]
    [HasPermission(SellerPermissions.CreateBook)]
    [EndpointDescription("Add new book")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromForm]CreateBookRequest request,CancellationToken cancellationToken)
    {
        var response=await _bookServices.AddBookAsync(request,cancellationToken);
        return response.Match<IActionResult>(
            res=> CreatedAtAction(nameof(Get), new { id = res.id }, res),
            error=>error.ToProblem()
            );
    }
    [HttpGet("{id}")]
    [HasPermission(MemberPermissions.GetBooks)]
    [EndpointDescription("Get book by id")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromRoute]int id,CancellationToken cancellationToken)
    {
        var response = await _bookServices.GetBookByIdAsync(id, cancellationToken);
        return response.Match<IActionResult>(
            res => Ok(res),
            error => error.ToProblem());
    }
    [HttpPut("{id}")]
    [HasPermission(SellerPermissions.UpdateBook)]
    [EndpointDescription("Update book")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookRequest request, CancellationToken cancellationToken)
    {
        var response = await _bookServices.UpdateBookAsync(id,request, cancellationToken);
        return response.Match<IActionResult>(
             res => Ok(res),
             error => error.ToProblem());
    }
    [HttpPut("update-image-{id}")]
    [HasPermission(SellerPermissions.UpdateBook)]
    [EndpointDescription("Update book image")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateImage([FromRoute] int id, [FromForm] BookImageRequest request, CancellationToken cancellationToken)
    {
        var response = await _bookServices.UpdateBookImageAsync(id, request, cancellationToken);
        return response.Match<IActionResult>(
             res => Ok(res),
             error => error.ToProblem());
    }
    [HttpPut("update-document-{id}")]
    [HasPermission(SellerPermissions.UpdateBook)]
    [EndpointDescription("Update book document")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateDocument([FromRoute] int id, [FromForm] BookFileRequest request, CancellationToken cancellationToken)
    {
        var response = await _bookServices.UpdateBookFileAsync(id, request, cancellationToken);
        return response.Match<IActionResult>(
             res => Ok(res),
             error => error.ToProblem());
    }
    [HttpPut("toggel-status-{id}")]
    [HasPermission(SellerPermissions.UpdateBook)]
    [EndpointDescription("Toggel book status")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddToggel([FromRoute]int id,CancellationToken cancellationToken)
    {
        var response = await _bookServices.ToggleBookAsync(id,cancellationToken);
        return response.Match<IActionResult>(
             res => Ok(res),
             error => error.ToProblem());
    }
}
