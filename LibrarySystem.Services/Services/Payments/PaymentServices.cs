using LibrarySystem.Domain.DTO.Payments;
using Stripe;
using Stripe.Checkout;
namespace LibrarySystem.Services.Services.Payments
{
    public class PaymentServices(IUnitOfWork unitOfWork) : IPaymentServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<SessionResponse> CreateCheckoutSessionAsync(PaymentOrderRequest request, CancellationToken cancellationToken = default)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"https://localhost:7157/SuccessfulPaymens.html/{request.OrderId}",
                CancelUrl = "https://localhost:7157/CancelPaymens.html",
            };
            foreach (var item in request.Items)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                        },
                        UnitAmount = (long)(item.Price * 100), // Amount in cents
                    },
                    Quantity = item.Quantity,
                });
            }
            var service = new SessionService();
            Session session = await service.CreateAsync(options,cancellationToken:cancellationToken);

            await _unitOfWork.OrderRepository.SetPaymentIdAndPaymentIntentId(request.OrderId, session.PaymentIntentId,session.Id);
            return new SessionResponse(session.Id, session.Url, session.SuccessUrl, session.CancelUrl, session.PaymentIntentId);
        }
        public async Task<OneOf<bool, Error>> ConfirmOrderPaymentStatus(int orderId, CancellationToken cancellationToken = default)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order is null)
                return OrderErrors.NotFound;

            var service = new SessionService();
            var session = await service.GetAsync(order.sessionId,cancellationToken:cancellationToken);
            if (session.PaymentStatus == PaymentStatuss.paid)
            {
                order.Status = OrderStatuss.Completed;
                order.PaymentStatus = PaymentStatuss.paid;
                order.PaymentIntentId = session.PaymentIntentId;
                await _unitOfWork.SaveChanges(cancellationToken);
                return true;
            }
            return PaymentErrors.NotPaid;
        }
        public async Task<OneOf<bool, Error>> RefundPaymentStatus(int orderId,CancellationToken cancellationToken=default)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order is null)
                return OrderErrors.NotFound;

            var options = new RefundCreateOptions
            {
                PaymentIntent = order!.PaymentIntentId,
            };

            var service = new RefundService();
            Refund refund = await service.CreateAsync(options, cancellationToken: cancellationToken);

            if (refund.Status ==PaymentStatuss.succeeded)
            {
                order.PaymentStatus = PaymentStatuss.Refunded;
                order.Status = OrderStatuss.Cancelled;
                await _unitOfWork.SaveChanges(cancellationToken);
                return true;
            }

            return PaymentErrors.Refunded;
        }
    }
}