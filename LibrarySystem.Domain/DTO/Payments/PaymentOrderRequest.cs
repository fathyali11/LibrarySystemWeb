namespace LibrarySystem.Domain.DTO.Payments;
public record  PaymentOrderRequest(
    int OrderId,
    decimal TotalAmount,
    List<PaymentItemRequest> Items
    );
