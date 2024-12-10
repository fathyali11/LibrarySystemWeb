using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Data.EntityConfigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Comment)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(r => r.Rating)
                .IsRequired();

            builder.HasOne(u=>u.User)
                .WithMany(r=>r.Reviews)
                .HasForeignKey(u=>u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b=>b.Book)
                .WithMany(r=>r.Reviews)
                .HasForeignKey(b=>b.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
