namespace LibrarySystem.Domain.DTO.Books;
public record BookRequest(
    string Title,
    string Description,
    int Quantity,
    decimal PriceForBuy,
    decimal PriceForBorrow,
    int CategoryId,
    int AuthorId
    );

