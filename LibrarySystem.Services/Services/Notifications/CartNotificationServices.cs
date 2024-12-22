namespace LibrarySystem.Services.Services.Notifications;
public class CartNotificationServices(IUnitOfWork unitOfWork) : ICartNotificationServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task RemoveCompletedAsync()
    {
        var orders = await _unitOfWork.OrderRepository.GetOrdersByStatusAsync(CancellationToken.None);
        foreach (var order in orders)
        {
            await _unitOfWork.CartRepository.RemoveCompletedAsync(order.UserId, CancellationToken.None);
        }
        await _unitOfWork.SaveChanges(CancellationToken.None);
    }
}
