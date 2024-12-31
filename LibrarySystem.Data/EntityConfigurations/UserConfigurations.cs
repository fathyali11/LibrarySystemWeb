using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using Microsoft.AspNetCore.Identity;
namespace LibrarySystem.Data.EntityConfigurations;
public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasIndex(u => new { u.UserName, u.Email });

        builder.Property(u => u.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Address)
            .HasMaxLength(250)
            .IsRequired();

        builder.HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Payments)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Cart)
            .WithOne(u => u.User)
            .HasForeignKey<Cart>(c => c.UserId);

        builder.OwnsMany(x => x.RefreshTokens)
            .ToTable("RefreshTokens")
            .WithOwner()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(r=>r.Reviews)
            .WithOne(u=>u.User)
            .HasForeignKey(u=>u.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.Fines)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // seed data
        builder.HasData([
             new ApplicationUser
             {
                 Id=ManagerUser.Id,
                 UserName=ManagerUser.UserName,
                 NormalizedUserName=ManagerUser.NormalizedUserName,
                 Email=ManagerUser.Email,
                 NormalizedEmail=ManagerUser.NormalizedEmail,
                 EmailConfirmed=ManagerUser.EmailConfirmed,
                 PasswordHash="AQAAAAIAAYagAAAAEPklm/Ltfr+4YA6KCMrEy5jzHaBjk8O36UZjV2UMVTO+leHN8pBEMVJrNAOsZ+868g==",
                 SecurityStamp=ManagerUser.SecurityStamp,
                 ConcurrencyStamp=ManagerUser.ConcurrencyStamp,
                 PhoneNumber=ManagerUser.PhoneNumber,
                 PhoneNumberConfirmed=ManagerUser.PhoneNumberConfirmed,
                 TwoFactorEnabled=ManagerUser.TwoFactorEnabled,
                 LockoutEnd=ManagerUser.LockoutEnd,
                 LockoutEnabled=ManagerUser.LockoutEnabled,
                 AccessFailedCount=ManagerUser.AccessFailedCount,
                 FirstName=ManagerUser.FirstName,
                 LastName=ManagerUser.LastName,
                 Address=ManagerUser.Address
             },
             new ApplicationUser
             {
                    Id=SellerUser.Id,
                    UserName=SellerUser.UserName,
                    NormalizedUserName=SellerUser.NormalizedUserName,
                    Email=SellerUser.Email,
                    NormalizedEmail=SellerUser.NormalizedEmail,
                    EmailConfirmed=SellerUser.EmailConfirmed,
                    PasswordHash="AQAAAAIAAYagAAAAEPklm/Ltfr+4YA6KCMrEy5jzHaBjk8O36UZjV2UMVTO+leHN8pBEMVJrNAOsZ+868g==",
                    SecurityStamp=SellerUser.SecurityStamp,
                    ConcurrencyStamp=SellerUser.ConcurrencyStamp,
                    PhoneNumber=SellerUser.PhoneNumber,
                    PhoneNumberConfirmed=SellerUser.PhoneNumberConfirmed,
                    TwoFactorEnabled=SellerUser.TwoFactorEnabled,
                    LockoutEnd=SellerUser.LockoutEnd,
                    LockoutEnabled=SellerUser.LockoutEnabled,
                    AccessFailedCount=SellerUser.AccessFailedCount,
                    FirstName=SellerUser.FirstName,
                    LastName=SellerUser.LastName,
                    Address=SellerUser.Address
             }
            ]);

    }
}
