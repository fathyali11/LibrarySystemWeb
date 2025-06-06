﻿namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("token")]
public class BorrowedBooksController(IBorrowedBookServices borrowedBookServices) : ControllerBase
{
    private readonly IBorrowedBookServices _borrowedBookServices = borrowedBookServices;
    [HttpPut("return{borrowedBookId}")]
    [HasPermission(MemberPermissions.ReturnBorrowedBooks)]
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
    [HasPermission(SellerPermissions.GetBorrowedBooks)]
    public async Task<IActionResult> GetAllBorrowedBooksAsync(CancellationToken cancellationToken = default)
    {
        var result = await _borrowedBookServices.GetAllBorrowedBooksAsync(cancellationToken);
        return Ok(result);
    }
}
