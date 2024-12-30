using System.Linq.Expressions;
using LibrarySystem.Domain.DTO.BorrowBooks;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.IRepository
{
    public interface IBorrowedBookRepository:IGenericRepository<BorrowedBook>
    {
        Task<List<BorrowBookReminderNotificationResponse>> GetAllWithUserAndBook(Expression<Func<BorrowedBook, bool>> predicate);
        Task<List<BorrowBookFineNotificationResponse>> GetAllWithUserAndBookForFine(Expression<Func<BorrowedBook, bool>> predicate);
        Task RemoveAsync(string userId, CancellationToken cancellationToken = default);
        Task <List<UserBorrowBookForFine>> GetAllBooksAndUser(Expression<Func<BorrowedBook, bool>> predicate);
        Task<int> ReturningAndGetBookId(int borrowedBookId, CancellationToken cancellationToken = default);
        Task<List<borrowedBookResponse>> GetAllWithUserAndBookForDisplay(CancellationToken cancellationToken = default);
        Task AddToFines(int id, int fineId, CancellationToken cancellationToken = default);
    }
}
