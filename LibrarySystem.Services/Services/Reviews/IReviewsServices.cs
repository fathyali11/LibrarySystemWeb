using LibrarySystem.Domain.Abstractions.Pagination;
using LibrarySystem.Domain.DTO.Common;
using LibrarySystem.Domain.DTO.Reviews;

namespace LibrarySystem.Services.Services.Reviews;
public interface IReviewsServices:IReviewsRepository
{
    Task<OneOf<ReviewResponse, Error>> AddReviewAsync(string userId, ReviewRequest request, CancellationToken cancellationToken = default);
    Task<OneOf<ReviewResponse, Error>> GetReviewAsync(string userId, int id, CancellationToken cancellationToken = default);
    Task<OneOf<PaginatedResult<Review, ReviewResponse>, Error>> GetAllReviewsAsync(PaginatedRequest request, CancellationToken cancellationToken = default);
    Task<OneOf<PaginatedResult<Review, ReviewResponse>, Error>> GetReviewsAsync(string userId, PaginatedRequest request, CancellationToken cancellationToken = default);
    Task<OneOf<ReviewResponse, Error>> UpdateReviewAsync(string userId, int id, ReviewRequest request, CancellationToken cancellationToken = default);
    Task<OneOf<bool, Error>> DeleteReviewAsync(string userId, int id, CancellationToken cancellationToken = default);
}
