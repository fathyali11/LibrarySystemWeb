using System.Security.Claims;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using LibrarySystem.Domain.DTO.Common;
using LibrarySystem.Domain.DTO.Reviews;
using LibrarySystem.Services.CustomAuthorization;
using LibrarySystem.Services.Services.Reviews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReviewsController(IReviewsServices reviewsServices) : ControllerBase
{
    private readonly IReviewsServices _reviewsServices= reviewsServices;

    [HttpPost("")]
    [HasPermission(MemberPermissions.AddReview)]
    public async Task<IActionResult> AddReview(ReviewRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _reviewsServices.AddReviewAsync(userId,request, cancellationToken);
        return result.Match<IActionResult>(
            review => CreatedAtAction(nameof(GetReview), new {Id=review.Id},review),
            error => error.ToProblem());
    }
    [HttpGet("{id}")]
    [HasPermission(MemberPermissions.GetReviews)]
    public async Task<IActionResult> GetReview(int id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _reviewsServices.GetReviewAsync(userId,id, cancellationToken);
        return result.Match<IActionResult>(
            review => Ok(review),
            error => error.ToProblem());
    }
    [HttpGet("")]
    [HasPermission(MemberPermissions.GetReviews)]
    public async Task<IActionResult> GetReviews([FromQuery]PaginatedRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _reviewsServices.GetReviewsAsync(userId!,request, cancellationToken);
        return result.Match<IActionResult>(
            reviews => Ok(reviews),
            error => error.ToProblem());
    }
    [HttpGet("all")]
    [HasPermission(ManagerPermissions.GetAllReviews)]
    public async Task<IActionResult> GetAllReviews([FromQuery] PaginatedRequest request, CancellationToken cancellationToken)
    {
        var result = await _reviewsServices.GetAllReviewsAsync(request, cancellationToken);
        return result.Match<IActionResult>(
            reviews => Ok(reviews),
            error => error.ToProblem());
    }
    [HttpPut("{id}")]
    [HasPermission(MemberPermissions.UpdateReview)]
    public async Task<IActionResult> UpdateReview(int id, ReviewRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _reviewsServices.UpdateReviewAsync(userId,id, request, cancellationToken);
        return result.Match<IActionResult>(
            review => Ok(review),
            error => error.ToProblem());
    }
    [HttpDelete("{id}")]
    [HasPermission(MemberPermissions.DeleteReview)]
    public async Task<IActionResult> DeleteReview(int id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _reviewsServices.DeleteReviewAsync(userId,id, cancellationToken);
        return result.Match<IActionResult>(
            review => Ok(review),
            error => error.ToProblem());
    }
}
