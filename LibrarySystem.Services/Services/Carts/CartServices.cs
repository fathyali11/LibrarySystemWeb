using LibrarySystem.Domain.DTO.Carts;
namespace LibrarySystem.Services.Services.Carts;
/// <include file='ExternalServicesDocs\CartServicesDocs.xml' path='/docs/members[@name="cartServices"]/CartServices'/>
public class CartServices(ApplicationDbContext context,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    HybridCache hybridCache):CartRepository(context), ICartServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly IMapper _mapper=mapper;
    private readonly HybridCache _hybridCache = hybridCache;
    /// <include file='ExternalServicesDocs\CartServicesDocs.xml' path='/docs/members[@name="cartServices"]/GetCartAsync'/>
    public async Task<OneOf<CartResponse, Error>> GetCartAsync(int id, CancellationToken cancellationToken = default)
    {
        CartResponse cached;
        cached = await _hybridCache.GetOrCreateAsync($"{GeneralConsts.CartCachedKey}{id}",
            async cartEntity =>
            {
                var cart = await _unitOfWork.CartRepository.GetCartWithItems(id,false, cancellationToken);
                return _mapper.Map<CartResponse>(cart);
            });
        return cached;
    }
    /// <include file='ExternalServicesDocs\CartServicesDocs.xml' path='/docs/members[@name="cartServices"]/ClearCartAsync'/>
    public async Task<OneOf<bool, Error>> ClearCartAsync(int id, CancellationToken cancellationToken = default)
    {
        var cartFromDb = await _unitOfWork.CartRepository.GetCartWithItems(id, false,cancellationToken);
        if (cartFromDb is null)
            return CartErrors.NotFound;

        foreach(var item in cartFromDb.CartItems)
        {
            item.Book.Quantity += item.Quantity;
        }
        cartFromDb.TotalAmount = 0;
        _unitOfWork.CartItemRepository.DeleteRange(cartFromDb.CartItems);
        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync($"{GeneralConsts.CartCachedKey}{id}");
        await _hybridCache.RemoveAsync(GeneralConsts.AllBooksCachedKey, cancellationToken);
        await _hybridCache.RemoveAsync(GeneralConsts.AllAvailableBooksCachedKey, cancellationToken);
        return true;

    }


}
