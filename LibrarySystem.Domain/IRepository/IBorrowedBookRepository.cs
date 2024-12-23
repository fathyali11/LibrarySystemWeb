using System.Linq.Expressions;
using LibrarySystem.Domain.DTO.BorrowBooks;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.IRepository
{
    public interface IBorrowedBookRepository:IGenericRepository<BorrowedBook>
    {
        Task<List<BorrowBookNotificationResponse>> GetAllWithUserAndBook(Expression<Func<BorrowedBook, bool>> predicate);
        Task RemoveAsync(string userId, CancellationToken cancellationToken = default);
    }
}
