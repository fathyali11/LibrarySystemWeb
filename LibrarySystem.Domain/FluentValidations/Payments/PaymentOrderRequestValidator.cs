using FluentValidation;
using LibrarySystem.Domain.DTO.Payments;

namespace LibrarySystem.Domain.FluentValidations.Payments;
public class PaymentOrderRequestValidator:AbstractValidator<PaymentOrderRequest>
{
    public PaymentOrderRequestValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("OrderId must be greater than 0");
        RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("TotalAmount must be greater than 0");
        RuleForEach(x=>x.Items).SetValidator(new PaymentItemRequestValidator());
    }
}
