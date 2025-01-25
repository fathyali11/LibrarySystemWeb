using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.Abstractions.Errors
{
    public static class ReviewErrors
    {
        public static readonly Error NotFound = new("Review.NotFound", "This review is not found", StatusCodes.Status401Unauthorized);
        



    }

}
