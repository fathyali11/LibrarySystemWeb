using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Abstractions.Pagination;
using LibrarySystem.Domain.DTO.Common;
using LibrarySystem.Domain.DTO.Reviews;

namespace LibrarySystem.Services.Services.Reviews;
public class ReviewsServices(ApplicationDbContext context,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    HybridCache hybridCache) : ReviewsRepository(context), IReviewsServices
{
    private readonly IUnitOfWork _unitOfWork= unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly HybridCache _hybridCache = hybridCache;
    public async Task<OneOf<ReviewResponse,Error>> AddReviewAsync(string userId, ReviewRequest request,CancellationToken cancellationToken=default)
    {
        var bookIsExist = await _unitOfWork.BookRepository.IsExits(x=>x.Id == request.BookId, cancellationToken);
        if(!bookIsExist)
            return BookErrors.NotFound;
        var review = _mapper.Map<Review>(request);
        review.UserId = userId;
        await _unitOfWork.ReviewsRepository.AddAsync(review, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        return _mapper.Map<ReviewResponse>(review);
    }
    public async Task<OneOf<ReviewResponse,Error>> GetReviewAsync(string userId, int id, CancellationToken cancellationToken = default)
    {
        var review = await _unitOfWork.ReviewsRepository.GetByAsync(x => x.Id == id&&x.UserId==userId, cancellationToken:cancellationToken);
        if (review == null)
            return ReviewErrors.NotFound;
        return _mapper.Map<ReviewResponse>(review);
    }
    public async Task<OneOf<PaginatedResult<Review,ReviewResponse>, Error>> GetAllReviewsAsync(PaginatedRequest request,CancellationToken cancellationToken = default)
    {
        const string cacheKey = "Reviews";
        var reviews = await _hybridCache.GetOrCreateAsync(cacheKey,
          async _ =>
          {
              var query = _unitOfWork.ReviewsRepository.GetAll(cancellationToken: cancellationToken);
              var reviewsFromDb = await query.ToListAsync(cancellationToken);
              return reviewsFromDb;
          });

        if(!string.IsNullOrEmpty(request.SearchTerm))
            reviews = reviews.Where(x => x.Comment.Contains(request.SearchTerm,StringComparison.OrdinalIgnoreCase)).ToList();

        if(!string.IsNullOrEmpty(request.SortTerm))
            reviews =reviews.AsQueryable()
                .OrderBy($"{request.SortTerm} {request.SortBy}")
                .ToList();

        var paginatedReviews=PaginatedResult<Review, ReviewResponse>.Create(reviews, request.PageNumber, request.PageSize);
        paginatedReviews.Result=_mapper.Map<List<ReviewResponse>>(paginatedReviews.Values);
        return paginatedReviews;
    }
    public async Task<OneOf<PaginatedResult<Review, ReviewResponse>, Error>> GetReviewsAsync(string userId,PaginatedRequest request, CancellationToken cancellationToken = default)
    {
        const string cacheKey = "MyReviews";
        var reviews = await _hybridCache.GetOrCreateAsync(cacheKey,
          async _ =>
          {
              var query = _unitOfWork.ReviewsRepository.GetAll(x=>x.UserId==userId,cancellationToken: cancellationToken);
              var reviewsFromDb = await query.ToListAsync(cancellationToken);
              return reviewsFromDb;
          });

        if (!string.IsNullOrEmpty(request.SearchTerm))
            reviews = reviews.Where(x => x.Comment.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

        if (!string.IsNullOrEmpty(request.SortTerm))
            reviews = reviews.AsQueryable()
                .OrderBy($"{request.SortTerm} {request.SortBy}")
                .ToList();

        var paginatedReviews = PaginatedResult<Review, ReviewResponse>.Create(reviews, request.PageNumber, request.PageSize);
        paginatedReviews.Result = _mapper.Map<List<ReviewResponse>>(paginatedReviews.Values);
        return paginatedReviews;
    }
    public async Task<OneOf<ReviewResponse, Error>> UpdateReviewAsync(string userId, int id, ReviewRequest request, CancellationToken cancellationToken = default)
    {
        var reviewFromDb = await _unitOfWork.ReviewsRepository.GetByAsync(x => x.Id == id&&x.UserId==userId, cancellationToken: cancellationToken);
        if (reviewFromDb == null)
            return ReviewErrors.NotFound;
        _mapper.Map(request, reviewFromDb);
        await _unitOfWork.SaveChanges(cancellationToken);
        return _mapper.Map<ReviewResponse>(reviewFromDb);
    }
    public async Task<OneOf<bool, Error>> DeleteReviewAsync(string userId, int id, CancellationToken cancellationToken = default)
    {
        var reviewFromDb = await _unitOfWork.ReviewsRepository.GetByAsync(x => x.Id == id && x.UserId == userId, cancellationToken: cancellationToken);
        if (reviewFromDb == null)
            return ReviewErrors.NotFound;
        _unitOfWork.ReviewsRepository.Delete(reviewFromDb);
        await _unitOfWork.SaveChanges(cancellationToken);
        return true;
    }


}
