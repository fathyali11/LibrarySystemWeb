using LibrarySystem.Domain.DTO.Carts;

namespace LibrarySystem.Services.Services.Carts;
public class CartServices(ApplicationDbContext context,
    IUnitOfWork unitOfWork,
    IMapper mapper):CartRepository(context), ICartServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly IMapper _mapper=mapper;
    public async Task<OneOf<CartResponse, Error>> GetCartAsync(int id, CancellationToken cancellationToken = default)
    {
        var cartFromDb=await _unitOfWork.CartRepository.GetCartWithItems(id, cancellationToken);
        if(cartFromDb is null) 
            return CartErrors.NotFound;

        var response=_mapper.Map<CartResponse>(cartFromDb);
        return response;
    }
    public async Task<OneOf<bool, Error>> ClearCartAsync(int id, CancellationToken cancellationToken = default)
    {
        var cartFromDb = await _unitOfWork.CartRepository.GetCartWithItems(id, cancellationToken);
        if (cartFromDb is null)
            return CartErrors.NotFound;

        foreach(var item in cartFromDb.CartItems)
        {
            item.Book.Quantity += item.Quantity;
        }
        cartFromDb.TotalAmount = 0;
        _unitOfWork.CartItemRepository.DeleteRange(cartFromDb.CartItems);
        await _unitOfWork.SaveChanges(cancellationToken);
        return true;

    }
}
