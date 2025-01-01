using Microsoft.AspNetCore.Authorization;

namespace LibrarySystem.Services.CustomAuthorization;
// optional class for custom authorization
// we can use Authorize[policyName:permission]
public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{

}
