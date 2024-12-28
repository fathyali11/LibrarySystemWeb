namespace LibrarySystem.Domain.DTO.Fines;
public record FineResponse(
    string FirstName,
    string LastName,
    string Email,
    string BookTitle,
    DateTime DueAt,
    decimal Amount,
    decimal TotalAmount
    );
