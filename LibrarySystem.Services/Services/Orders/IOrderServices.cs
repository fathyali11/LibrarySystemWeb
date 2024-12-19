using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.DTO.Orders;
using OneOf;

namespace LibrarySystem.Services.Services.Orders
{
    public interface IOrderServices
    {
        Task<OneOf<OrderResponse, Error>> MakeOrderAsync(int cartId, CancellationToken cancellationToken = default);
        Task<OneOf<bool, Error>> CancelOrderAsync(int id, CancellationToken cancellationToken = default);
        Task<OneOf<OrderResponse, Error>> GetOrderAsync(string userId, CancellationToken cancellationToken = default);

    }
}
