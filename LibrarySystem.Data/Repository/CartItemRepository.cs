using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repository;
public class CartItemRepository(ApplicationDbContext context) : GenericRepository<CartItem>(context), ICartItemRepository
{
    private readonly ApplicationDbContext _context=context;
    public async Task<CartItem?> GetWithBookAndCartAsync(int id, CancellationToken cancellationToken = default)
    {
        var orderItemFromDb=await _context.CartItems.Where(x=>x.Id==id)
            .Include(x=>x.Book)
            .Include(x=>x.Cart)
            .SingleOrDefaultAsync(cancellationToken);

        return orderItemFromDb;
    }
}
