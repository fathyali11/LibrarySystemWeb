using LibrarySystem.Domain.DTO.Books;

namespace LibrarySystem.Domain.DTO.Orders;
public record OrderRequest(
    List<BookOrderRequest> Books
    );