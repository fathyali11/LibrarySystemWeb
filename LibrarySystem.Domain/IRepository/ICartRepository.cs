using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.IRepository;
public interface ICartRepository:IGenericRepository<Cart>
{
    Task<Cart?> GetCartWithItems(int id,CancellationToken cancellationToken=default);
}
