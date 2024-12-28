using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Data.EntityConfigurations
{
    public class FineConfiguration : IEntityTypeConfiguration<Fine>
    {
        public void Configure(EntityTypeBuilder<Fine> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Amount)
                .IsRequired();
            builder.Property(f=>f.CreatedAt)
                .IsRequired();

            builder.HasOne(f => f.User)
                .WithMany(u=> u.Fines)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b=> b.BorrowedBook)
                .WithOne(bb => bb.Fine)
                .HasForeignKey<Fine>(f => f.BorrowBookId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
