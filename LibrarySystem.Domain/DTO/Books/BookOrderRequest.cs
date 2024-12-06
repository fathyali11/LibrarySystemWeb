namespace LibrarySystem.Domain.DTO.Books;
public record BookOrderRequest(
    int BookId,
    string Type,
    int Quantity,
    int CategoryId,
    int AuthorId
    );