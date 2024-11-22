using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LibrarySystem.Data.EntityConfigurations;
public class BookConfigurations : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.AuthorId)
            .IsRequired();

        builder.Property(b => b.Quantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(b => b.PriceForBorrow)
            .IsRequired()
            .HasColumnType("decimal(10, 2)");

        builder.Property(b => b.PriceForBuy)
            .IsRequired()
            .HasColumnType("decimal(10, 2)");

        builder.HasIndex(b => b.Title)
            .IsUnique();
    }
}
