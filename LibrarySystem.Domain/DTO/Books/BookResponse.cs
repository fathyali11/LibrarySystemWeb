namespace LibrarySystem.Domain.DTO.Books;
public record BookResponse(
    int id,
    string Title,
    string Description,
    int Quantity,
    decimal PriceForBuy,
    decimal PriceForBorrow,
    bool IsActive,
    DateTime PublishedDate,
    int CategoryId,
    int AuthorId,
    string FilePath,
    string ImagePath
    );
