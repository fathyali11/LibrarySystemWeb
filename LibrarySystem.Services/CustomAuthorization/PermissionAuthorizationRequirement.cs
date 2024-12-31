using Microsoft.AspNetCore.Authorization;

namespace LibrarySystem.Services.CustomAuthorization;
public class PermissionAuthorizationRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
