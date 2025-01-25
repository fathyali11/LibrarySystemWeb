using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.Abstractions.Errors;
public static class RoleErrors
{
    public readonly static Error NotExist = new Error("Roles.IsNotExist","Role is not exist",StatusCodes.Status400BadRequest);
    public readonly static Error Exist = new Error("Roles.IsExist","Role is already exist",StatusCodes.Status409Conflict);
}
