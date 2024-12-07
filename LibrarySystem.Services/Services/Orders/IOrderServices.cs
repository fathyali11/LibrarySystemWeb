using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.DTO.Orders;
using OneOf;

namespace LibrarySystem.Services.Services.Orders
{
    public interface IOrderServices
    {
        Task<OneOf<OrderResponse, Error>> AddOrderAsync(string userId,OrderRequest request,CancellationToken cancellationToken=default);
        Task<OneOf<List<OrderResponse>, Error>> GetAllOrdersAsync(string userId,CancellationToken cancellationToken=default);
        Task<bool> RemoveOrdersAsync(int id, string userId, CancellationToken cancellationToken = default);

        Task<OneOf<bool,Error>> ConfirmOrderAsync(int id,string userId, CancellationToken cancellationToken=default);







    }
}
