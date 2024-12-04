using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Services.Services.OrderItems
{
    public interface IOrderItemServices
    {
        Task<OrderItem> AddOrderItemAsync(BookOrderRequest request, Book book, CancellationToken cancellationToken = default);
        Task<OrderItem> PlusAsync(int id, CancellationToken cancellationToken = default);
        Task<OrderItem> MinusAsync(int id, CancellationToken cancellationToken = default);










    }
}
