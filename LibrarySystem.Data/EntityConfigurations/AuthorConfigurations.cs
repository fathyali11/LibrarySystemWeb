using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LibrarySystem.Data.EntityConfigurations;
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
            .WithOne()
            .HasForeignKey("AuthorId")
            .IsRequired() 
            .OnDelete(DeleteBehavior.Restrict); // Cascade delete: deleting an Author deletes their books
    }
}
