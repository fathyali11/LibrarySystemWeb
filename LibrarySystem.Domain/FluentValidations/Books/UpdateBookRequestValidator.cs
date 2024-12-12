using FluentValidation;
using LibrarySystem.Domain.DTO.Books;

namespace LibrarySystem.Domain.FluentValidations.Books;
public class UpdateBookRequestValidator : AbstractValidator<UpdateBookRequest>
{
    public UpdateBookRequestValidator()
    {

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");

        RuleFor(x => x.PriceForBuy)
            .GreaterThan(0).WithMessage("Price for buy must be greater than zero.");

        RuleFor(x => x.PriceForBorrow)
            .GreaterThan(0).WithMessage("Price for borrow must be greater than zero.");

    }
}