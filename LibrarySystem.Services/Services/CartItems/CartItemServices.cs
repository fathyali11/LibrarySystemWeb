using LibrarySystem.Domain.DTO.CartItems;

namespace LibrarySystem.Services.Services.CartItems;
public class CartItemServices(ApplicationDbContext context,
    IUnitOfWork unitOfWork,
    IMapper mapper) : CartItemRepository(context), ICartItemServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<OneOf<bool,Error>> AddOrderToCartAsync(string userId,CartItemRequest request, CancellationToken cancellationToken = default)
    {
        var cartFromDb = await _unitOfWork.CartRepository.GetByAsync(x => x.UserId == userId, cancellationToken: cancellationToken);
        Cart cart = cartFromDb ?? new Cart { UserId = userId };

        if(cartFromDb is null) 
            await _unitOfWork.CartRepository.AddAsync(cart);

        var categoryIsExists = await _unitOfWork.CategoryRepository.IsExits(x => x.Id == request.CategoryId, cancellationToken);
        if (!categoryIsExists)
            return CategoryErrors.NotFound;

        var authorIsExists = await _unitOfWork.AuthorRepository.IsExits(x => x.Id == request.AuthorId, cancellationToken);
        if (!authorIsExists)
            return AuthorErrors.NotFound;

        var bookFromDb=await _unitOfWork.BookRepository.GetByAsync(x=>x.Id==request.BookId, cancellationToken: cancellationToken);
        if(bookFromDb is null) 
            return BookErrors.NotFound;

        if (bookFromDb.Quantity < request.Quantity)
            return OrderErrors.NotEnoughQuantity;
        bookFromDb.Quantity-=request.Quantity;
        var price = string.Equals(request.Type, OrderTypes.Buy, StringComparison.OrdinalIgnoreCase) ?
            bookFromDb.PriceForBuy : bookFromDb.PriceForBorrow;

        var itemFromDb=await _unitOfWork.CartItemRepository.GetByAsync(x=>x.BookId==request.BookId&&x.CartId==cart.Id, cancellationToken:cancellationToken);   
        CartItem cartItem=itemFromDb??_mapper.Map<CartItem>(request);
        cartItem.Price = price;

        if(itemFromDb is null)
            cart.CartItems.Add(cartItem);

        cart.TotalAmount += (price * request.Quantity);

        await _unitOfWork.SaveChanges(cancellationToken);

        return true;
    }

    public async Task<OneOf<bool, Error>> PlusAsync(string userId, int id, CancellationToken cancellationToken = default)
    {
        var cartItemFromDb = await _unitOfWork.CartItemRepository
            .GetWithBookAndCartAsync(id,cancellationToken);
        if(cartItemFromDb is null)
            return OrderErrors.NotFound;


        
        if(cartItemFromDb.Book.Quantity <= 0)
            return OrderErrors.NotEnoughQuantity;

        cartItemFromDb.Book.Quantity -= 1;
        cartItemFromDb.Quantity++;

        cartItemFromDb.Cart.TotalAmount += (cartItemFromDb.Price);
        await _unitOfWork.SaveChanges(cancellationToken);
        return true;
    }



}
