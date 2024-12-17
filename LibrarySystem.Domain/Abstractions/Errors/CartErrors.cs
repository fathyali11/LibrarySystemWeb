using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.Abstractions.Errors
{
    public static class CartErrors
    {
        public static readonly Error NotFound = new("Cart.NotFound", "This Cart is not found", StatusCodes.Status400BadRequest);










    }
}
