using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repository;
public class CartRepository(ApplicationDbContext context) : GenericRepository<Cart>(context), ICartRepository
{
    private readonly ApplicationDbContext _context=context;

    public async Task<Cart?> GetCartWithItems(int id, CancellationToken cancellationToken = default)
    {
        var cart = await _context.Carts
            .Where(x=>x.Id == id)
            .Include(x=>x.CartItems)
            .ThenInclude(x=>x.Book)
            .SingleOrDefaultAsync(cancellationToken);

        return cart;
    }
}
