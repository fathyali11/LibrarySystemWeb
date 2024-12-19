using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.IRepository
{
    public interface IOrderItemRepository:IGenericRepository<OrderItem>
    {
        Task AddRange(IEnumerable<OrderItem> items);
    }
}
