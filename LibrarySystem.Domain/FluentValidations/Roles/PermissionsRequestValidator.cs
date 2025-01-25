using FluentValidation;
using LibrarySystem.Domain.DTO.Roles;

namespace LibrarySystem.Domain.FluentValidations.Roles;

public class PermissionsRequestValidator : AbstractValidator<PermissionsRequest>
{
    public PermissionsRequestValidator()
    {
        RuleForEach(RuleFor => RuleFor.Permissions).NotEmpty().WithMessage("Permission is required");
    }
}
