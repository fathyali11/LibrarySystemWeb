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
    }
}
