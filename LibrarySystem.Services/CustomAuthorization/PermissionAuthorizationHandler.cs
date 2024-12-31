using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using Microsoft.AspNetCore.Authorization;

namespace LibrarySystem.Services.CustomAuthorization;
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == ManagerPermissions.Type && c.Value == requirement.Permission)&& context.User.Identity?.IsAuthenticated == true)
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
