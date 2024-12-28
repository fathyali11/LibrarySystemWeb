namespace LibrarySystem.Domain.DTO.BorrowBooks;
public record BorrowBookFineNotificationResponse(
    int Id,
    string FirstName,
    string LastName,
    string UserId,
    string Email,
    string BookTitle,
    DateTime DueDate,
    decimal FineAmount,
    decimal TotalFineAmount
    );