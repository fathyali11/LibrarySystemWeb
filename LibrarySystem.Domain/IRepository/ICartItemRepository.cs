using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.IRepository;
public interface ICartItemRepository:IGenericRepository<CartItem>
{
    Task<CartItem?> GetWithBookAndCartAsync(int id,CancellationToken cancellationToken=default);
}
