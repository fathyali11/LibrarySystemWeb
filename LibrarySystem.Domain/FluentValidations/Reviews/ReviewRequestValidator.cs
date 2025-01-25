using FluentValidation;
using LibrarySystem.Domain.DTO.Reviews;

namespace LibrarySystem.Domain.FluentValidations.Reviews;
public class ReviewRequestValidator: AbstractValidator<ReviewRequest>
{
    public ReviewRequestValidator()
    {
        RuleFor(x => x.BookId).NotEmpty().WithMessage("BookId is required")
            .GreaterThan(0).WithMessage("BookId must be greater than 0");
        RuleFor(x => x.Rating).InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5");

    }
}
