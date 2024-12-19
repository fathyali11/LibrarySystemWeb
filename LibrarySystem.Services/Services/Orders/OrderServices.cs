using LibrarySystem.Domain.DTO.Orders;

namespace LibrarySystem.Services.Services.Orders
{
    public class OrderServices(IUnitOfWork unitOfWork,
        IMapper mapper,
        HybridCache hybridCache) : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork=unitOfWork;
        private readonly IMapper _mapper=mapper;
        private readonly HybridCache _hybridCache = hybridCache;
        private readonly string _orderCachKey = "order-";

        public async Task<OneOf<OrderResponse, Error>> MakeOrderAsync(int cartId, CancellationToken cancellationToken = default)
        {
            var cartFromDb= await _unitOfWork.CartRepository.GetCartWithItems(cartId, true,cancellationToken);
            var order = _mapper.Map<Order>(cartFromDb);
            
            foreach(var item in order.OrderItems)
            {
                if(string.Equals(item.OrderType , OrderTypes.Borrow,StringComparison.OrdinalIgnoreCase)) 
                    await HandleBorrowedBookAsync(item.BookId,order.UserId,cancellationToken);
                item.Id = 0;
            }

            await _unitOfWork.OrderRepository.AddAsync(order,cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);
            await _hybridCache.RemoveAsync($"{_orderCachKey}{order.UserId}", cancellationToken);
            var response =_mapper.Map<OrderResponse>(order);
            return response;
        }
        public async Task<OneOf<OrderResponse, Error>> GetOrderAsync(string userId,CancellationToken cancellationToken = default)
        {
            OrderResponse cached ;
            cached = await _hybridCache.GetOrCreateAsync($"{_orderCachKey}{userId}",
                async orderEntity =>
                {
                    var order = await _unitOfWork.OrderRepository.GetByIdWithBooksAsync(userId, cancellationToken);
                    return _mapper.Map<OrderResponse>(order);
                });

            if (cached == null)
                return OrderErrors.NotFound;

            return cached;
        }
        public async Task<OneOf<bool, Error>> CancelOrderAsync(int id,CancellationToken cancellationToken = default)
        {
            var orderFromDb = await _unitOfWork.OrderRepository.GetByAsync(x=>x.Id==id,"OrderItems",cancellationToken);
            if(orderFromDb == null) 
                return OrderErrors.NotFound;
            await _unitOfWork.BorrowedBookRepository.RemoveAsync(orderFromDb!.UserId,cancellationToken);
            _unitOfWork.OrderItemRepository.DeleteRange(orderFromDb.OrderItems);
            _unitOfWork.OrderRepository.Delete(orderFromDb);
            await _hybridCache.RemoveAsync($"{_orderCachKey}{orderFromDb.UserId}", cancellationToken);

            await _unitOfWork.SaveChanges(cancellationToken);
            return true;
        }


        private async Task HandleBorrowedBookAsync(int bookId,string userId,CancellationToken cancellationToken)
        {
            var borrowBook = new BorrowedBook
            {
                UserId = userId,
                BookId = bookId,
            };
            await _unitOfWork.BorrowedBookRepository.AddAsync(borrowBook,cancellationToken);
        }
    }
}
