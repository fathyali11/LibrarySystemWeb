using FluentValidation;
using LibrarySystem.Domain.DTO.ApplicationUsers;

namespace LibrarySystem.Domain.FluentValidations.ApplicationUsers;
public class ChangeUserRoleRequestValidator: AbstractValidator<ChangeUserRoleRequest>
{
    public ChangeUserRoleRequestValidator()
    {
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .MaximumLength(100).WithMessage("Role must not exceed 100 characters.");
    }
}
