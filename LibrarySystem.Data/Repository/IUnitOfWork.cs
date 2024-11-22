

using LibrarySystem.Domain.IRepository;

namespace LibrarySystem.Data.Repository;
public interface IUnitOfWork
{
    IBookRepository BookRepository { get; }
    Task SaveChanges(CancellationToken cancellationToken = default);
}
