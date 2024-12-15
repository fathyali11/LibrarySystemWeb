namespace LibrarySystem.Services.Services.CartItems;
public class CartItemServices(ApplicationDbContext context) : CartItemRepository(context), ICartItemServices
{

}
