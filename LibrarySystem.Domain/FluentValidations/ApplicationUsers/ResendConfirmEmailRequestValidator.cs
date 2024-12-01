
using FluentValidation;
using LibrarySystem.Domain.DTO.ApplicationUsers;

namespace LibrarySystem.Domain.FluentValidations.ApplicationUsers
{
    public class ResendConfirmEmailRequestValidator:AbstractValidator<ResendConfirmEmailRequest>
    {
        public ResendConfirmEmailRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .Must(IsGmailAddress).WithMessage("Email must be a Gmail address.");
        }
        private bool IsGmailAddress(string email) =>
             email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase);
    }
}
