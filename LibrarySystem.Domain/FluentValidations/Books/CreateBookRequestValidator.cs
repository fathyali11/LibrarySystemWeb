using FluentValidation;
using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.FluentValidations.GeneralValiations;


namespace LibrarySystem.Domain.FluentValidations.Books;
public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
{
    public CreateBookRequestValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");

        RuleFor(x => x.PriceForBuy)
            .GreaterThan(0).WithMessage("Price for buy must be greater than zero.");

        RuleFor(x => x.PriceForBorrow)
            .GreaterThan(0).WithMessage("Price for borrow must be greater than zero.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category ID must be a positive integer.");

        RuleFor(x => x.AuthorId)
            .GreaterThan(0).WithMessage("Author ID must be a positive integer.");

        RuleFor(x => x.Document)
            .SetValidator(new DocumentValidator());

        RuleFor(x => x.Image)
           .SetValidator(new ImageValidator());

    }
}