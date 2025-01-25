using FluentValidation;
using LibrarySystem.Domain.DTO.Roles;

namespace LibrarySystem.Domain.FluentValidations.Roles;
public class RoleRequestValidator:AbstractValidator<RoleRequest>
{
    public RoleRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Role name is required");
    }
}
