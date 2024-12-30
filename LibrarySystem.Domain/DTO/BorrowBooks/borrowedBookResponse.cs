namespace LibrarySystem.Domain.DTO.BorrowBooks;
public record borrowedBookResponse(
    int Id,
    string Title,
    int BookId,
    string UserName,
    DateTime ?ReturnDate,
    DateTime DueDate
);
