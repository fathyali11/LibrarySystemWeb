using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.Abstractions.Errors;
public static class PaymentErrors
{
    public static readonly Error Refunded = new Error("Payment.IsRefunded","Payment has been refunded",StatusCodes.Status400BadRequest);
    public static readonly Error NotPaid = new Error("Payment.NotPaid","Payment has not been paid",StatusCodes.Status400BadRequest);
}
