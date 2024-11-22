using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LibrarySystem.Data.EntityConfigurations;
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
            .HasColumnType("decimal(10, 2)")
            .IsRequired();

        builder.Property(p => p.PaymentDate)
            .IsRequired();

        builder.Property(p => p.PaymentMethod)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.PaymentStatus)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasOne(p => p.Order)
            .WithMany()
            .HasForeignKey(p => p.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
