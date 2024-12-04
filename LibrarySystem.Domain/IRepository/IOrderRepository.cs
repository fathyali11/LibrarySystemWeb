using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.IRepository
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<List<Order>> GetAllWithBooksAsync(CancellationToken cancellationToken = default);
    }
}
