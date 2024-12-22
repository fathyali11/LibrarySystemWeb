using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Abstractions.ConstValues;
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
        public async Task<Order> GetByIdWithBooksAsync(string userId,CancellationToken cancellationToken = default)
        {
            var orders = await _context.Orders
                .Where(o=>o.UserId==userId)
                .Include(o => o.OrderItems)
                .ThenInclude(b => b.Book)
                .ToListAsync(cancellationToken);
            return orders.SingleOrDefault()!;
        }
        public async Task<bool> UpdateStatusAsync(int orderId)
        {
            var res= await _context.Orders
                .Where(x=>x.Id== orderId)
                .ExecuteUpdateAsync(x=>x.SetProperty(pro=>pro.Status,OrderStatuss.Completed));
            return res > 0;
        }
        public async Task SetPaymentIdAndPaymentIntentId(int orderId, string paymentIntentId, string sessionId)
        {
            await _context.Orders
                .Where(x => x.Id == orderId)
                .ExecuteUpdateAsync(x=>x.SetProperty(pro=>pro.sessionId, sessionId)
                    .SetProperty(pro => pro.PaymentIntentId, paymentIntentId));
        }
    }
}
