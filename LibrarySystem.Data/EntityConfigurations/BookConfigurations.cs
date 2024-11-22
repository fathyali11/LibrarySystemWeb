using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

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

        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
