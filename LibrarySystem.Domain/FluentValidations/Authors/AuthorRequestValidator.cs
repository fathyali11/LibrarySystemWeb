using FluentValidation;
using LibrarySystem.Domain.DTO.Author;
namespace LibrarySystem.Domain.FluentValidations.Authors;
public class AuthorRequestValidator : AbstractValidator<AuthorRequest>
{
    public AuthorRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty().WithMessage("Author name is required.")
            .MaximumLength(100).WithMessage("Author name must not exceed 100 characters.");

        RuleFor(x => x.Biography)
            .NotNull()
            .NotEmpty().WithMessage("Biography is required.")
            .MaximumLength(1000).WithMessage("Biography must not exceed 1000 characters.");
    }
}