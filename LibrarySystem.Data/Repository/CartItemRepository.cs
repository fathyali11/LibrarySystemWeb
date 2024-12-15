using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;

namespace LibrarySystem.Data.Repository;
public class CartItemRepository(ApplicationDbContext context) : GenericRepository<CartItem>(context), ICartItemRepository
{

}
