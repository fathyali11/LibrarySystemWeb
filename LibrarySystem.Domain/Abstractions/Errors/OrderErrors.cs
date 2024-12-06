using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.Abstractions.Errors
{
    public static class OrderErrors
    {
        public static readonly Error NotFound = new("Order.NotFound", "This Order is not found", StatusCodes.Status400BadRequest);










    }
}
