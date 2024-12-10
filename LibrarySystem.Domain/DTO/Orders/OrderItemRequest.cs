namespace LibrarySystem.Domain.DTO.Orders;
public record OrderItemRequest(
    int BookId,
    string Type,
    int Quantity,
    int CategoryId,
    int AuthorId
    );