using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.IRepository;
public interface ICartRepository:IGenericRepository<Cart>
{
    Task<Cart?> GetCartWithItems(int id, bool includeUser = false, CancellationToken cancellationToken = default);

}
