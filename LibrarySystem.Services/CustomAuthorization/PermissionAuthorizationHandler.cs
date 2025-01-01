using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using Microsoft.AspNetCore.Authorization;

namespace LibrarySystem.Services.CustomAuthorization;
// This class is used by the AuthorizationHandler to check if the user has the required permission
// it takes the permission from the AuthorizationRequirement
// it checks if the user has the permission
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    // This method is called when the AuthorizationHandler needs to determine
    // if a user meets the requirements of a specific authorization policy.
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == ManagerPermissions.Type && c.Value == requirement.Permission)&&
            context.User.Identity?.IsAuthenticated == true)
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
