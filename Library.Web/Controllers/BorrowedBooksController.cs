using System.Security.Claims;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Services.Services.BorrowedBooks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BorrowedBooksController(IBorrowedBookServices borrowedBookServices) : ControllerBase
{
    private readonly IBorrowedBookServices _borrowedBookServices = borrowedBookServices;
    [HttpPut("return{borrowedBookId}")]
    [EndpointDescription("Return a borrowed book")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [EndpointSummary("this endpoint take id and return this book")]
    public async Task<IActionResult> ReturnBookAsync(int borrowedBookId, CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _borrowedBookServices.ReturnBookAsync(userId!, borrowedBookId, cancellationToken);
        return result.Match<IActionResult>(
            success => Ok(),
            error=>error.ToProblem()
            );
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAllBorrowedBooksAsync(CancellationToken cancellationToken = default)
    {
        var result = await _borrowedBookServices.GetAllBorrowedBooksAsync(cancellationToken);
        return Ok(result);
    }
}
