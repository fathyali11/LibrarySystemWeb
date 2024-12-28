namespace LibrarySystem.Domain.DTO.Fines;
public record FineResponse(
    string FirstName,
    string LastName,
    string Email,
    List<string> BooksTitle,
    DateTime DueAt,
    decimal Amount,
    decimal TotalAmount
    );
