using Microsoft.Extensions.Caching.Hybrid;
using System.Threading;

namespace LibrarySystem.Services.Services.Notifications;
public class CartNotificationServices(IUnitOfWork unitOfWork) : ICartNotificationServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task RemoveCompletedAsync()
    {
        //var orders = await _unitOfWork.OrderRepository.GetOrdersByStatusAsync(CancellationToken.None);
        //foreach (var order in orders)
        //{
        //    await _unitOfWork.CartRepository.RemoveCompletedAsync(order.UserId, CancellationToken.None);
        //    await _hybridCache.RemoveAsync($"{GeneralConsts.CartCachedKey}{id}");
        //    await _hybridCache.RemoveAsync(GeneralConsts.AllBooksCachedKey, cancellationToken);
        //    await _hybridCache.RemoveAsync(GeneralConsts.AllAvailableBooksCachedKey, cancellationToken);
        //}
        //await _unitOfWork.SaveChanges(CancellationToken.None);
    }
}
