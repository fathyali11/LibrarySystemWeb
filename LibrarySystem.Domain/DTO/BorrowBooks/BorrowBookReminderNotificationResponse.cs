namespace LibrarySystem.Domain.DTO.BorrowBooks;
public record BorrowBookReminderNotificationResponse(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string BookTitle,
    DateTime DueDate
    );