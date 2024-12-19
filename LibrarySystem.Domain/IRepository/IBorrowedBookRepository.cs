using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.IRepository
{
    public interface IBorrowedBookRepository:IGenericRepository<BorrowedBook>
    {
        Task RemoveAsync(string userId, CancellationToken cancellationToken = default);
    }
}
