using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class BookConfigurations : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasIndex(b => b.Title)
            .IsUnique();

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);


        builder.Property(b => b.FilePath)
           .IsRequired();

        builder.Property(b => b.ImagePath)
           .IsRequired();

        builder.Property(b => b.ImageName)
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

    }
}
