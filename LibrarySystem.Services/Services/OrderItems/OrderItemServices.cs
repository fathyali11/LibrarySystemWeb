//using LibrarySystem.Data.Repository;
//using LibrarySystem.Domain.Abstractions;
//using LibrarySystem.Domain.Abstractions.ConstValues;
//using LibrarySystem.Domain.Abstractions.Errors;
//using LibrarySystem.Domain.DTO.Books;
//using LibrarySystem.Domain.Entities;
//using LibrarySystem.Services.Services.Cashing;
//using OneOf;

//namespace LibrarySystem.Services.Services.OrderItems
//{
//    public class OrderItemServices(IUnitOfWork unitOfWork,ICacheServices cacheServices):IOrderItemServices
//    {
//        private readonly IUnitOfWork _unitOfWork = unitOfWork;
//        private readonly ICacheServices _cacheServices = cacheServices;

//        public async Task<OrderItem> AddOrderItemAsync(BookOrderRequest request,Book book, string userId, CancellationToken cancellationToken = default)
//        {
//                var orderItem = new OrderItem
//                {
//                    BookId = request.BookId,
//                    Quantity = request.Quantity,
//                    Price = string.Equals(request.Type, OrderTypes.Borrow, StringComparison.OrdinalIgnoreCase) ?
//                   book.PriceForBorrow : book.PriceForBuy,
//                    Book = book
//                };

//                var response=await _unitOfWork.OrderItemRepository.AddAsync(orderItem);
//                await _cacheServices.RemoveAsync($"order-{userId}", cancellationToken);
//            return response!;
//        }
//        public async Task<OneOf<OrderItem,Error>> PlusAsync(int id, string userId, CancellationToken cancellationToken=default)
//        {
//            var orderItem=await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
//            var bookFromDb = await _unitOfWork.BookRepository.ExitsOrNot(x => (x.Id == orderItem!.BookId && x.IsAvailable && x.IsActive), cancellationToken);

//            if (bookFromDb!.Quantity<=0)
//                return OrderErrors.NotEnoughQuantity;
//            orderItem!.Quantity++;
//            var book=await _unitOfWork.BookRepository.GetByIdAsync(orderItem.BookId);
//            book!.Quantity--;
//            var order= await _unitOfWork.OrderRepository.GetByIdAsync(orderItem.OrderId);
//            order!.TotalAmount += orderItem.Price;
//            await _unitOfWork.SaveChanges(cancellationToken);
//            await _cacheServices.RemoveAsync($"order-{userId}", cancellationToken);
//            return orderItem;
//        }
//        public async Task<OrderItem> MinusAsync(int id, string userId, CancellationToken cancellationToken = default)
//        {
//            var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
//            if(orderItem!.Quantity>0)
//            {
//                var book = await _unitOfWork.BookRepository.GetByIdAsync(orderItem.BookId);
//                book!.Quantity++;
//                var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderItem.OrderId);
//                order!.TotalAmount -= orderItem.Price;
//                orderItem.Quantity--;
//            }
//            await _unitOfWork.SaveChanges(cancellationToken);
//            await _cacheServices.RemoveAsync($"order-{userId}", cancellationToken);
//            return orderItem;
//        }
//        public async Task<bool> RemoveAsync(int id, string userId, CancellationToken cancellationToken=default)
//        {
//            var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
//            var book = await _unitOfWork.BookRepository.GetByIdAsync(orderItem!.BookId);
//            book!.Quantity += orderItem.Quantity;
//            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderItem!.OrderId);
//            order!.TotalAmount-=(orderItem.Price*orderItem.Quantity);
//            _unitOfWork.OrderItemRepository.Delete(orderItem!);
//            await _unitOfWork.SaveChanges(cancellationToken);
//            await _cacheServices.RemoveAsync($"order-{userId}", cancellationToken);
//            return true;
//        }

//    }
//}
