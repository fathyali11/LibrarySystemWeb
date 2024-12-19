using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repository;
public class CartRepository(ApplicationDbContext context) : GenericRepository<Cart>(context), ICartRepository
{
    private readonly ApplicationDbContext _context=context;

    public async Task<Cart?> GetCartWithItems(int id, bool includeUser = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Carts
            .Where(c => c.Id == id);
        if (includeUser )
            query=query.Include(c => c.User);

        query=query.Include(x=>x.CartItems)
            .ThenInclude(x=>x.Book);
            

        return await query.SingleOrDefaultAsync(cancellationToken);
    }
}
