using System.Threading.Tasks;
using LibrarySystem.Domain.DTO.Payments;
using Stripe.Checkout;

namespace LibrarySystem.Services.Services.Payments;
public interface IPaymentServices
{
    Task<SessionResponse> CreateCheckoutSessionAsync(PaymentOrderRequest request,CancellationToken cancellationToken=default);
    Task<OneOf<bool,Error>> ConfirmOrderPaymentStatus(int orderId,CancellationToken cancellationToken=default);
    Task<OneOf<bool, Error>> RefundPaymentStatus(int orderId, CancellationToken cancellationToken = default);
}
