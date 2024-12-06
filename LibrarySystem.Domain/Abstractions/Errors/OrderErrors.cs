using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.Abstractions.Errors
{
    public static class OrderErrors
    {
        public static readonly Error NotFound = new("Order.NotFound", "This Order is not found", StatusCodes.Status400BadRequest);
        public static readonly Error NotEnoughQuantity = new("Order.LargeQuantity", "This Order quantity greater than quantity of this book! please decrease it", StatusCodes.Status400BadRequest);










    }
}
