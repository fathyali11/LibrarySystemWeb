

using LibrarySystem.Domain.IRepository;

namespace LibrarySystem.Data.Repository;
public interface IUnitOfWork
{
    IBookRepository BookRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    Task SaveChanges(CancellationToken cancellationToken = default);
}
