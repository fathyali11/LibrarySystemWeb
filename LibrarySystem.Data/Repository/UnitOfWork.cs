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

    public IBookRepository BookRepository =>
        _bookRepository ?? new BookRepository(_context, _mapper);
    public ICategoryRepository CategoryRepository=>
        _categoryRepository ?? new CategoryRepository(_context,_mapper);

    public IAuthorRepository AuthorRepository=>
        _authorRepository ?? new AuthorRepository(_context,_mapper);

    public async Task SaveChanges(CancellationToken cancellationToken=default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
