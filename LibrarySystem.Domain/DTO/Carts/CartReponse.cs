using LibrarySystem.Domain.DTO.Items;

namespace LibrarySystem.Domain.DTO.Carts;
public record CartResponse
{
    public int Id { get; init; }
    public decimal TotalPrice { get; init; }
    public List<ItemResponse> CartItems { get; init; } = new List<ItemResponse>();

    public CartResponse() { }

    public CartResponse(int id, decimal totalPrice, List<ItemResponse> cartItems)
    {
        Id = id;
        TotalPrice = totalPrice;
        CartItems = cartItems ?? new List<ItemResponse>();
    }
}

