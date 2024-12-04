using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;

namespace LibrarySystem.Data.Repository
{
    public class OrderItemRepository(ApplicationDbContext context) : GenericRepository<OrderItem>(context), IOrderItemRepository
    {
    }
}
