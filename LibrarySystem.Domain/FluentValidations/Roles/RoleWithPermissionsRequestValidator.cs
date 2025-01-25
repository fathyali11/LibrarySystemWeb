using FluentValidation;
using LibrarySystem.Domain.DTO.Roles;

namespace LibrarySystem.Domain.FluentValidations.Roles;
public class RoleWithPermissionsRequestValidator : AbstractValidator<RoleWithPermissionsRequest>
{
    public RoleWithPermissionsRequestValidator()
    {
        RuleFor(RuleFor => RuleFor.Name).NotEmpty().WithMessage("Role name is required");
        RuleForEach(RuleFor => RuleFor.Permissions).NotEmpty().WithMessage("Permission is required");
    }
}
