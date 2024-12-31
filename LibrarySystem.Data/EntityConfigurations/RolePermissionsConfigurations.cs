using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Data.EntityConfigurations;
public class RolePermissionsConfigurations : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        var managerPermissions=ManagerPermissions.AllPermissions.ToList();
        var memberPermissions = MemberPermissions.AllPermissions.ToList();
        var sellerPermissions = SellerPermissions.AllPermissions.ToList();

        // seed manager permissions
        int idCounter = 1; // Start ID counter
        foreach (var permission in managerPermissions)
        {
            builder.HasData(new IdentityRoleClaim<string>
            {
                Id = idCounter++,
                RoleId = ManagerRole.Id,
                ClaimType = ManagerPermissions.Type,
                ClaimValue = permission
            });
        }

        // Seed member permissions
        foreach (var permission in memberPermissions)
        {
            builder.HasData(new IdentityRoleClaim<string>
            {
                Id = idCounter++,
                RoleId = MemberRole.Id,
                ClaimType = MemberPermissions.Type,
                ClaimValue = permission
            });
        }

        // Seed seller permissions
        foreach (var permission in sellerPermissions)
        {
            builder.HasData(new IdentityRoleClaim<string>
            {
                Id = idCounter++,
                RoleId = SellerRole.Id,
                ClaimType = SellerPermissions.Type,
                ClaimValue = permission
            });
        }

    }
}
