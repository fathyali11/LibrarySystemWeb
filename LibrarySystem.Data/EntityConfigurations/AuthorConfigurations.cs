using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class AuthorConfigurations : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasIndex(a => a.Name)
            .IsUnique();

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Biography)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasMany(a => a.Books)
            .WithOne(b => b.Author) 
            .HasForeignKey(b => b.AuthorId) 
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
