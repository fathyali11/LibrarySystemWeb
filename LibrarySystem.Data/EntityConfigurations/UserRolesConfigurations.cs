using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Data.EntityConfigurations;
public class UserRolesConfigurations : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData([
            new IdentityUserRole<string>
            {
                RoleId =ManagerRole.Id,
                UserId = ManagerUser.Id,
            },
            new IdentityUserRole<string>
            {
                 RoleId =SellerRole.Id,
                UserId = SellerUser.Id,
            }
        ]);
    }
}
