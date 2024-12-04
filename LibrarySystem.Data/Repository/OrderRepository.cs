using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repository
{
    public class OrderRepository(ApplicationDbContext context) : GenericRepository<Order>(context), IOrderRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<List<Order>> GetAllWithBooksAsync(CancellationToken cancellationToken=default)
        {
            var orders=await _context.Orders
                .Include(o=>o.OrderItems)
                .ThenInclude(b=>b.Book)
                .ToListAsync(cancellationToken);
            return orders;
        }
    }
}
