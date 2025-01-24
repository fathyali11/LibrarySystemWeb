using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace LibrarySystem.Data.EntityConfigurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.CreatedAt)
            .IsRequired();

        builder.Property(o => o.TotalAmount)
            .HasColumnType("decimal(10, 2)")
            .IsRequired();

        builder.Property(o => o.Status)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(o => o.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(o => o.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(o => o.Address)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(o => o.Email)
           .HasMaxLength(256)
           .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
