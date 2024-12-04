using LibrarySystem.Data.Repository;
using LibrarySystem.Domain.Abstractions.ConstValues;
using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Services.Services.OrderItems
{
    public class OrderItemServices(IUnitOfWork unitOfWork):IOrderItemServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<OrderItem> AddOrderItemAsync(BookOrderRequest request,Book book, CancellationToken cancellationToken = default)
        {
                var orderItem = new OrderItem
                {
                    BookId = request.BookId,
                    Quantity = request.Quantity,
                    Price = string.Equals(request.Type, OrderTypes.Borrow, StringComparison.OrdinalIgnoreCase) ?
                   book.PriceForBorrow : book.PriceForBuy,
                    Book = book
                };

                var response=await _unitOfWork.OrderItemRepository.AddAsync(orderItem);
                return response!;
        }
        public async Task<OrderItem> PlusAsync(int id,CancellationToken cancellationToken=default)
        {
            var orderItem=await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
            orderItem!.Quantity++;
            await _unitOfWork.SaveChanges(cancellationToken);
            return orderItem;
        }
        public async Task<OrderItem> MinusAsync(int id, CancellationToken cancellationToken = default)
        {
            var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
            if(orderItem!.Quantity>0)
            {
                orderItem.Quantity--;
            }
            await _unitOfWork.SaveChanges(cancellationToken);
            return orderItem;
        }
    }
}
