using LibrarySystem.Domain.DTO.CartItems;

namespace LibrarySystem.Services.Services.CartItems;
public interface ICartItemServices:ICartItemRepository
{
    Task<OneOf<bool,Error>> AddOrderToCartAsync(string userId,CartItemRequest request,CancellationToken cancellationToken=default);
    Task<OneOf<bool,Error>> PlusAsync(string userId,int id,CancellationToken cancellationToken=default);
    

}
