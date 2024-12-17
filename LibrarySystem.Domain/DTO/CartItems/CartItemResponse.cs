namespace LibrarySystem.Domain.DTO.CartItems;
public record CartItemResponse(
    int Id,
    string Name,
    decimal Price,
    int Quantity,
    decimal TotalPrice,
    string Type,
    string ImageUrl
    );