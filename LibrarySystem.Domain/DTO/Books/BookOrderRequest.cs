namespace LibrarySystem.Domain.DTO.Books;
public record BookOrderRequest(
    int BookId,
    string Type,
    int Quantity,
    decimal Price,
    int CategoryId,
    int AuthorId
    );