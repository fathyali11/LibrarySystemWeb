using System.Linq.Expressions;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.IRepository
{
    public interface IBorrowedBookRepository:IGenericRepository<BorrowedBook>
    {
        Task<List<BorrowedBook>> GetAllWithUserAndBook(Expression<Func<BorrowedBook, bool>> predicate);
        Task RemoveAsync(string userId, CancellationToken cancellationToken = default);
    }
}
