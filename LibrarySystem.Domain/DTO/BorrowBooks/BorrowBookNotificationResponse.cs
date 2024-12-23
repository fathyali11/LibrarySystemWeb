namespace LibrarySystem.Domain.DTO.BorrowBooks;
public record BorrowBookNotificationResponse(
    string FirstName,
    string LastName,
    string Email,
    string BookTitle,
    DateTime DueDate
    );