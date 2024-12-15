using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;

namespace LibrarySystem.Data.Repository;
public class CartRepository(ApplicationDbContext context) : GenericRepository<Cart>(context), ICartRepository
{
}
