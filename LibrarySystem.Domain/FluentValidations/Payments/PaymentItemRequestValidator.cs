using FluentValidation;
using LibrarySystem.Domain.DTO.Payments;

namespace LibrarySystem.Domain.FluentValidations.Payments;
public class PaymentItemRequestValidator:AbstractValidator<PaymentItemRequest>
{
    public PaymentItemRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
    }
}
