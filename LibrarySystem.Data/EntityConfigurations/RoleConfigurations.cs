using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Data.EntityConfigurations;
public class RoleConfigurations : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(
           [
            new ApplicationRole
                {
                    Id = SellerRole.Id,
                    Name =SellerRole.Name,
                    NormalizedName = SellerRole.NormalizedName,
                    ConcurrencyStamp = SellerRole.ConcurrencyStamp,
                    IsMember = false,
                },
                new ApplicationRole
                {
                    Id = ManagerRole.Id,
                    Name = ManagerRole.Name,
                    NormalizedName = ManagerRole.NormalizedName,
                    ConcurrencyStamp = ManagerRole.ConcurrencyStamp,
                    IsMember = false,
                },
                new ApplicationRole
                {
                    Id = MemberRole.Id,
                    Name = MemberRole.Name,
                    NormalizedName = MemberRole.NormalizedName,
                    ConcurrencyStamp = MemberRole.ConcurrencyStamp,
                    IsMember = true,
                }
            ]
        );
    }
}
