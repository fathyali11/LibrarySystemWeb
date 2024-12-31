using Microsoft.AspNetCore.Authorization;

namespace LibrarySystem.Services.CustomAuthorization;
public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}
