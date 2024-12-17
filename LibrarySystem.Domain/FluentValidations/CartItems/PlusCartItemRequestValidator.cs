using FluentValidation;
using LibrarySystem.Domain.DTO.CartItems;

namespace LibrarySystem.Domain.FluentValidations.CartItems;
public class PlusCartItemRequestValidator:AbstractValidator<PlusCartItemRequest>
{
    public PlusCartItemRequestValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty().WithMessage("CartId is required")
            .GreaterThan(0).WithMessage("CartId must be greater than zero");

        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("OrderId is required")
            .GreaterThan(0).WithMessage("OrderId must be greater than zero");

    }
}
