﻿using LibrarySystem.Domain.DTO.CartItems;
using LibrarySystem.Domain.DTO.Carts;

namespace LibrarySystem.Services.Services.CartItems;
/// <include file='ExternalServicesDocs\CartItemDocs.xml' path='/docs/members[@name="cartItemServices"]/CartItemServices'/>
public class CartItemServices(ApplicationDbContext context,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    HybridCache hybridCache) : CartItemRepository(context), ICartItemServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly HybridCache _hybridCache=hybridCache;
    /// <include file='ExternalServicesDocs\CartItemDocs.xml' path='/docs/members[@name="cartItemServices"]/AddOrderToCartAsync'/>
    public async Task<OneOf<CartIdResponse, Error>> AddOrderToCartAsync(string userId,CartItemRequest request, CancellationToken cancellationToken = default)
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

        var itemFromDb = await _unitOfWork.CartItemRepository.GetByAsync(x => x.BookId == request.BookId && x.CartId == cart.Id, cancellationToken: cancellationToken);
        CartItem cartItem = itemFromDb ?? _mapper.Map<CartItem>(request);

        if(itemFromDb is not null)
        {
            if(itemFromDb.Quantity>request.Quantity)
            {
                bookFromDb.Quantity += (itemFromDb.Quantity - request.Quantity);
                cart.TotalAmount -= (itemFromDb.Price * (itemFromDb.Quantity - request.Quantity));
            }
            else
            {
                if (bookFromDb.Quantity < request.Quantity)
                    return OrderErrors.NotEnoughQuantity;

                bookFromDb.Quantity -= (request.Quantity - itemFromDb.Quantity);
                cart.TotalAmount += (itemFromDb.Price * (request.Quantity - itemFromDb.Quantity));
            }
            cartItem.Quantity = request.Quantity;
        }
        else
        {
            cart.CartItems.Add(cartItem);

            if (bookFromDb.Quantity < request.Quantity)
                return OrderErrors.NotEnoughQuantity;
            bookFromDb.Quantity -= request.Quantity;

            var price = string.Equals(request.Type, OrderTypes.Buy, StringComparison.OrdinalIgnoreCase) ?
                bookFromDb.PriceForBuy : bookFromDb.PriceForBorrow;


            cartItem.Price = price;
            cart.TotalAmount += (price * request.Quantity);
        }

        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync($"{GeneralConsts.CartCachedKey}{cart.Id}");
        await _hybridCache.RemoveAsync(GeneralConsts.AllBooksCachedKey, cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllAvailableBooksCachedKey, cancellationToken);
        return new CartIdResponse(cart.Id);
    }
    /// <include file='ExternalServicesDocs\CartItemDocs.xml' path='/docs/members[@name="cartItemServices"]/PlusAsync'/>
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
        await _hybridCache.RemoveAsync($"{GeneralConsts.CartCachedKey}{cartItemFromDb.CartId}");
        await _hybridCache.RemoveAsync(GeneralConsts.AllBooksCachedKey, cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllAvailableBooksCachedKey, cancellationToken);
        return true;
    }
    /// <include file='ExternalServicesDocs\CartItemDocs.xml' path='/docs/members[@name="cartItemServices"]/MinusAsync'/>
    public async Task<OneOf<bool, Error>> MinusAsync(string userId, int id, CancellationToken cancellationToken = default)
    {
        var cartItemFromDb = await _unitOfWork.CartItemRepository
            .GetWithBookAndCartAsync(id, cancellationToken);
        if (cartItemFromDb is null)
            return OrderErrors.NotFound;

        cartItemFromDb.Book.Quantity += 1;
        cartItemFromDb.Quantity--;

        cartItemFromDb.Cart.TotalAmount -= (cartItemFromDb.Price);
        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync($"{GeneralConsts.CartCachedKey}{cartItemFromDb.CartId}");
        await _hybridCache.RemoveAsync(GeneralConsts.AllBooksCachedKey, cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllAvailableBooksCachedKey, cancellationToken);
        return true;
    }
    /// <include file='ExternalServicesDocs\CartItemDocs.xml' path='/docs/members[@name="cartItemServices"]/RemoveAsync'/>
    public async Task<OneOf<bool, Error>> RemoveAsync(string userId, int id, CancellationToken cancellationToken = default)
    {
        var cartItemFromDb = await _unitOfWork.CartItemRepository
            .GetWithBookAndCartAsync(id, cancellationToken);
        if (cartItemFromDb is null)
            return OrderErrors.NotFound;


        cartItemFromDb.Book.Quantity += cartItemFromDb.Quantity;
        
        cartItemFromDb.Cart.TotalAmount -= (cartItemFromDb.Price*cartItemFromDb.Quantity);

        _unitOfWork.CartItemRepository.Delete(cartItemFromDb);

        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync($"{GeneralConsts.CartCachedKey}{cartItemFromDb.CartId}");
        await _hybridCache.RemoveAsync(GeneralConsts.AllBooksCachedKey, cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllAvailableBooksCachedKey, cancellationToken);
        return true;
    }

}
