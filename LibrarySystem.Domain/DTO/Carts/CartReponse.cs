using LibrarySystem.Domain.DTO.CartItems;

namespace LibrarySystem.Domain.DTO.Carts;
public record CartResponse
{
    public int Id { get; init; }
    public decimal TotalPrice { get; init; }
    public List<CartItemResponse> CartItems { get; init; } = new List<CartItemResponse>();

    public CartResponse() { }

    public CartResponse(int id, decimal totalPrice, List<CartItemResponse> cartItems)
    {
        Id = id;
        TotalPrice = totalPrice;
        CartItems = cartItems ?? new List<CartItemResponse>();
    }
}

