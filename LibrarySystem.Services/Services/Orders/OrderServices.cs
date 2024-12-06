using AutoMapper;
using LibrarySystem.Data.Repository;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.ConstValues;
using LibrarySystem.Domain.Abstractions.Errors;
using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.DTO.Orders;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Services.Services.OrderItems;
using OneOf;

namespace LibrarySystem.Services.Services.Orders
{
    public class OrderServices(IUnitOfWork unitOfWork,IMapper mapper,IOrderItemServices orderItemServices): IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork=unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IOrderItemServices _orderItemServices = orderItemServices;
        private const int _daysToReturnBorrowedBook = 14;
        public async Task<OneOf<OrderResponse, Error>> AddOrderAsync(string userId, OrderRequest request, CancellationToken cancellationToken = default)
        {
            var orderFromDb = await _unitOfWork.OrderRepository.GetByIdWithBooksAsync(userId,cancellationToken);
            Order order;
            if(orderFromDb is not null)
            {
                order = orderFromDb;
            }
            else
            {
                order = new Order()
                {
                    UserId = userId,
                    OrderStatus = OrderStatuss.Pending
                };
                await _unitOfWork.OrderRepository.AddAsync(order);
            }

            foreach (var bookRequest in request.Books)
            {
                var bookFromDb = await _unitOfWork.BookRepository.ExitsOrNot(x => (x.Id == bookRequest.BookId && x.IsAvailable && x.IsActive),cancellationToken);//IsAvailableAsync(bookRequest.BookId);
                if (bookFromDb is null)
                    return BookErrors.NotAvailable;
                if (bookRequest.Quantity > bookFromDb.Quantity)
                    return OrderErrors.NotEnoughQuantity;

                

                await HandelBorrowBookAsync(bookRequest, userId);

                
                var orderItemFromDb = await _unitOfWork.OrderItemRepository.ExitsOrNot(x => x.BookId == bookRequest.BookId,cancellationToken);
                if (orderItemFromDb is not null)
                {
                    if(orderItemFromDb.Quantity != orderItemFromDb.Quantity)
                        bookFromDb.Quantity -= bookRequest.Quantity;

                    orderItemFromDb.Quantity = bookRequest.Quantity;
                    continue;
                }
                bookFromDb.Quantity -= bookRequest.Quantity;
                var orderItem = await _orderItemServices.AddOrderItemAsync(bookRequest, bookFromDb, cancellationToken);
                order.OrderItems.Add(orderItem);
            }
            order.TotalAmount = CalculateTotalPrice(order);
            await _unitOfWork.SaveChanges(cancellationToken);

            var response = _mapper.Map<OrderResponse>(order);
            return response;
        }

        public async Task<OneOf<List<OrderResponse>, Error>> GetAllOrdersAsync(string userId, CancellationToken cancellationToken = default)
        {
            var orders = await _unitOfWork.OrderRepository.GetAllWithBooksAsync(cancellationToken);
            
            var response=_mapper.Map<List<OrderResponse>>(orders);
            return response;
        }

        public async Task<bool> RemoveOrdersAsync(int id,CancellationToken cancellationToken=default)
        {
            var order=await _unitOfWork.OrderRepository.GetByIdAsync(id);
            _unitOfWork.OrderRepository.Delete(order!);
            await _unitOfWork.SaveChanges(cancellationToken);
            return true;
        }

        public async Task<OneOf<bool, Error>> ConfirmOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if(order is null)
                return OrderErrors.NotFound;

            order.OrderStatus=OrderStatuss.Completed;
            await _unitOfWork.SaveChanges(cancellationToken);
            return true;
        }


        private async Task HandelBorrowBookAsync(BookOrderRequest book,string userId)
        {
            if (string.Equals(book.Type, OrderTypes.Borrow, StringComparison.OrdinalIgnoreCase))
            {
                var borrowBook = new BorrowedBook
                {
                    UserId = userId,
                    BookId = book.BookId,
                    BorrowDate = DateTime.UtcNow,
                    ReturnDate = DateTime.UtcNow.AddDays(_daysToReturnBorrowedBook)
                };
                await _unitOfWork.BorrowedBookRepository.AddAsync(borrowBook);
            }
        }
        private decimal CalculateTotalPrice(Order order)
        {
            decimal totalPrice = 0;
            foreach (var book in order.OrderItems)
                totalPrice += (book.Quantity * book.Price);
            return totalPrice;
        }
        
    }
}
