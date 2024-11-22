using AutoMapper;
using LibrarySystem.Data.Data;
using LibrarySystem.Domain.IRepository;

namespace LibrarySystem.Data.Repository;
public class UnitOfWork(ApplicationDbContext context,IMapper mapper) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper=mapper;
    private readonly IBookRepository ?_bookRepository;

    public IBookRepository BookRepository =>
        _bookRepository ?? new BookRepository(_context, _mapper);

    public async Task SaveChanges(CancellationToken cancellationToken=default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
