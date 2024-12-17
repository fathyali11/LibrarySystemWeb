using LibrarySystem.Domain.DTO.Carts;

namespace LibrarySystem.Services.Services.Carts;
public interface ICartServices:ICartRepository
{
    Task<OneOf<CartResponse,Error>> GetCartAsync(int id,CancellationToken cancellationToken=default);
    Task<OneOf<bool,Error>> ClearCartAsync(int id,CancellationToken cancellationToken=default);

}
