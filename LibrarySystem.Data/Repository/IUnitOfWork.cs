using LibrarySystem.Domain.IRepository;

namespace LibrarySystem.Data.Repository;
public interface IUnitOfWork
{
    IBookRepository BookRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IAuthorRepository AuthorRepository { get; }
    IUserRepository UserRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderItemRepository OrderItemRepository { get; }
    IBorrowedBookRepository BorrowedBookRepository { get; }
    ICartItemRepository CartItemRepository { get; }
    ICartRepository CartRepository { get; }
    Task SaveChanges(CancellationToken cancellationToken = default);
}
