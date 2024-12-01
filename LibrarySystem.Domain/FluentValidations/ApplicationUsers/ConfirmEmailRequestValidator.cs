using FluentValidation;
using LibrarySystem.Domain.DTO.ApplicationUsers;

namespace LibrarySystem.Domain.FluentValidations.ApplicationUsers
{
    public class ConfirmEmailRequestValidator:AbstractValidator<ConfirmEmailRequest>
    {
        public ConfirmEmailRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("user id should not be empty");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("token should not be empty");
        }
    }
}
