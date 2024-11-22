using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class AuthorConfigurations : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Biography)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasMany(a => a.Books)
            .WithOne(b => b.Author) // Specify navigation property
            .HasForeignKey(b => b.AuthorId) // Use the explicitly defined property
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); // Restrict instead of Cascade if desired
    }
}
