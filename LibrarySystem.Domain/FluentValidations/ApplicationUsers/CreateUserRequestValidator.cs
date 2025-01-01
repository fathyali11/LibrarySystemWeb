using FluentValidation;
using LibrarySystem.Domain.DTO.ApplicationUsers;

namespace LibrarySystem.Domain.FluentValidations.ApplicationUsers;
public class CreateUserRequestValidator:AbstractValidator<CreateOrUpdateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

        RuleFor(x => x.UserName)
        .NotEmpty().WithMessage("User Name is required.")
        .MaximumLength(100).WithMessage("User Name must not exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .Must(IsGmailAddress).WithMessage("Email must be a Gmail address.");

        RuleFor(x => x.Address)
            .MaximumLength(250).WithMessage("Address must not exceed 250 characters.");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(11)
            .MaximumLength(11)
            .WithMessage("Phone must not must be 11 digit.")
            .Must(IsValidPhoneNumber).WithMessage("Phone must be digits only and start with 01.");

        RuleFor(x => x.Password)
           .NotEmpty().WithMessage("Password is required.")
           .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
           .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
           .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
           .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
           .Matches(@"[!\@\#\$\%\^\&\*\(\)_\+\-]").WithMessage("Password must contain at least one special character.");

        RuleFor(x=>x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .MaximumLength(100).WithMessage("Role must not exceed 100 characters.");
    }
    private bool IsGmailAddress(string email) =>
         email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase);
    private bool IsValidPhoneNumber(string phone)
    {
        var containsCharacters = phone.Any(char.IsLetter);
        if (containsCharacters)
            return false;
        return phone.StartsWith("01");
    }
}
