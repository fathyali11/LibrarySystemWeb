namespace LibrarySystem.Domain.DTO.Books;
public record UpdateBookRequest(
    string Description,
    int Quantity,
    decimal PriceForBuy,
    decimal PriceForBorrow
    );

