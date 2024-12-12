using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.DTO.Books;
public record CreateBookRequest(
    string Description,
    int Quantity,
    decimal PriceForBuy,
    decimal PriceForBorrow,
    IFormFile Image,
    IFormFile Document,
    int CategoryId,
    int AuthorId
    );

