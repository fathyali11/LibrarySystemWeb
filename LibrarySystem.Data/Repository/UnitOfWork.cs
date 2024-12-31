using AutoMapper;
using LibrarySystem.Data.Data;
using LibrarySystem.Domain.IRepository;

namespace LibrarySystem.Data.Repository;
public class UnitOfWork(ApplicationDbContext context,IMapper mapper) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper=mapper;
    private readonly IBookRepository ?_bookRepository;
    private readonly ICategoryRepository? _categoryRepository;
    private readonly IAuthorRepository? _authorRepository;
    private readonly IUserRepository? _userRepository;
    private readonly IOrderRepository? _orderRepository;
    private readonly IOrderItemRepository? _orderItemRepository;
    private readonly IBorrowedBookRepository? _borrowedBookRepository;
    private readonly ICartItemRepository? _cartItemRepository;
    private readonly ICartRepository? _cartRepository;
    private readonly IFineRepository? _fineRepository;
    private readonly IRoleRepository? _roleRepository;

    public IBookRepository BookRepository =>
        _bookRepository ?? new BookRepository(_context, _mapper);
    public ICategoryRepository CategoryRepository=>
        _categoryRepository ?? new CategoryRepository(_context,_mapper);

    public IAuthorRepository AuthorRepository=>
        _authorRepository ?? new AuthorRepository(_context,_mapper);

    public IUserRepository UserRepository =>
        _userRepository ?? new UserRepository(_context);

    public IOrderRepository OrderRepository =>
        _orderRepository ?? new OrderRepository(_context);

    public IOrderItemRepository OrderItemRepository=>
        _orderItemRepository ?? new OrderItemRepository(_context);

    public IBorrowedBookRepository BorrowedBookRepository=>
        _borrowedBookRepository ?? new BorrowedBookRepository(_context);

    public ICartItemRepository CartItemRepository=>
        _cartItemRepository ?? new CartItemRepository(_context);

    public ICartRepository CartRepository =>
        _cartRepository ?? new CartRepository(_context);

    public IFineRepository FineRepository =>
        _fineRepository ?? new FineRepository(_context);

    public IRoleRepository RoleRepository =>
        _roleRepository ?? new RoleRepository(_context);
    public async Task SaveChanges(CancellationToken cancellationToken=default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
