using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.IRepository
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<List<Order>> GetAllWithBooksAsync(CancellationToken cancellationToken = default);
        Task<Order> GetByIdWithBooksAsync(string userId, CancellationToken cancellationToken = default);
        Task<bool> UpdateStatusAsync(int orderId);
        Task SetPaymentIdAndPaymentIntentId(int orderId, string paymentIntentId, string sessionId);
    }
}
 