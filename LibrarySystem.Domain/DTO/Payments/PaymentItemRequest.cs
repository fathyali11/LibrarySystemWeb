namespace LibrarySystem.Domain.DTO.Payments;
public record PaymentItemRequest(
    string Name,
    decimal Price,
    int Quantity
    );