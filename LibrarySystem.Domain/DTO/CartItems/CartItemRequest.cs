namespace LibrarySystem.Domain.DTO.CartItems;
public record CartItemRequest(
    int BookId,
    string Type,
    int Quantity,
    int CategoryId,
    int AuthorId
    );
