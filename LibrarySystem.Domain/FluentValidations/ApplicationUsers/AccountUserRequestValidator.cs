using FluentValidation;
using LibrarySystem.Domain.DTO.ApplicationUsers;

namespace LibrarySystem.Domain.FluentValidations.ApplicationUsers;
public class AccountUserRequestValidator:AbstractValidator<AccountUserRequest>
{
    public AccountUserRequestValidator()
    {
        RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

        RuleFor(x => x.Address)
            .MaximumLength(250).WithMessage("Address must not exceed 250 characters.");
    }
}