using Microsoft.AspNetCore.Authorization;

namespace LibrarySystem.Services.CustomAuthorization;

// read permission that user enterd in the attribute
// if this permission is not found in the policy, the policy will be created
// it sends the permission to the AuthorizationHandler
public class ApplicationAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
    : DefaultAuthorizationPolicyProvider(options)
{
    private readonly AuthorizationOptions _authorizationOptions = options.Value;

    // this method is called by the framework to get the policy
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        if (policy is not null)
            return policy;

        var permissionPolicy = new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionAuthorizationRequirement(policyName))
            .Build();

        _authorizationOptions.AddPolicy(policyName, permissionPolicy);

        return permissionPolicy;
    }
}