using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;

namespace LibrarySystem.Data.Repository
{
    public class OrderItemRepository(ApplicationDbContext context) : GenericRepository<OrderItem>(context), IOrderItemRepository
    {
        private readonly ApplicationDbContext _context=context;
        public async Task AddRange(IEnumerable<OrderItem> items)
        {
            await _context.OrderItems.AddRangeAsync(items);
        }
    }
}
