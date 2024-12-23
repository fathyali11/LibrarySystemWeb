using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LibrarySystem.Data.EntityConfigurations;
public class BorrowBookConfigurations : IEntityTypeConfiguration<BorrowedBook>
{
    public void Configure(EntityTypeBuilder<BorrowedBook> builder)
    {
        builder.HasKey(b => b.Id);

        
        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.BorrowDate)
            .IsRequired();

        builder.Property(b => b.DueDate)
            .IsRequired();

        builder.Property(b => b.ReturnDate)
            .IsRequired(false);

          

        builder.HasOne(b => b.Book) 
           .WithMany(b=>b.BorrowedBooks)
           .HasForeignKey(b => b.BookId)
           .IsRequired() 
           .OnDelete(DeleteBehavior.Restrict);

    }
}
