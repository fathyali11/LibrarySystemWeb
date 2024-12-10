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

        

        builder.Property(b => b.IsAvailable)
            .HasComputedColumnSql("CASE WHEN [Quantity] > 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", stored: false);

        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
