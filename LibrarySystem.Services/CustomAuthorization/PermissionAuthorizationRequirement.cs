using Microsoft.AspNetCore.Authorization;

namespace LibrarySystem.Services.CustomAuthorization;
// this class take it's permission from authorization provider
public class PermissionAuthorizationRequirement(string permission) : IAuthorizationRequirement
{
    // This class is used by the AuthorizationHandler to check if the user has the required permission
    public string Permission { get; } = permission;
}
