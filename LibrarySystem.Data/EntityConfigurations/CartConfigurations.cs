using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Data.EntityConfigurations
{
    public class CartConfigurations : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();


            builder.HasMany(c => c.CartItems)  
                .WithOne(ci => ci.Cart) 
                .HasForeignKey(ci => ci.CartId)  
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
