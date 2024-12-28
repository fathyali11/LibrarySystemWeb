using System.Linq;
using System.Linq.Expressions;
using LibrarySystem.Data.Data;
using LibrarySystem.Domain.DTO.BorrowBooks;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repository
{
    public class BorrowedBookRepository(ApplicationDbContext context) : GenericRepository<BorrowedBook>(context), IBorrowedBookRepository
    {
        private readonly ApplicationDbContext _context=context;
        public async Task<List<BorrowBookReminderNotificationResponse>> GetAllWithUserAndBook(Expression<Func<BorrowedBook, bool>> predicate)
        {
            var result =await _context.BorrowedBooks
                .Where(predicate)
                .Include(x => x.Book)
                .AsSplitQuery()
                .Include(x => x.User)
                .ThenInclude(x=>x.Fines)
                .AsSplitQuery()
                .Select(x=>new BorrowBookReminderNotificationResponse(
                    x.Id,
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Email!,
                    x.Book.Title,
                    x.DueDate
                    ))
                .AsNoTracking()
                .ToListAsync();

            return result;
        }
        public async Task<List<BorrowBookFineNotificationResponse>> GetAllWithUserAndBookForFine(Expression<Func<BorrowedBook, bool>> predicate)
        {
            var result = await _context.BorrowedBooks
                .Where(predicate)
                .Include(x => x.Book)
                .AsSplitQuery()

                .Include(x => x.User)
                .ThenInclude(x => x.Fines)
                .AsSplitQuery()

                .Select(x => new BorrowBookFineNotificationResponse(
                    x.Id,
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Id,
                    x.User.Email!,
                    x.Book.Title,
                    x.DueDate,
                    x.User.Fines!.FirstOrDefault(us=>us.UserId== x.UserId&&us.BorrowBookId==x.Id)!.Amount,
                    x.User.Fines!.FirstOrDefault(us=>us.UserId== x.UserId&&us.BorrowBookId==x.Id)!.TotalAmount
                    ))
                .AsNoTracking()
                .ToListAsync();

            return result;
        }
        public async Task RemoveAsync(string userId,CancellationToken cancellationToken=default)
        {
            await _context.BorrowedBooks
                .Where(x=>x.UserId == userId)
                .ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<List<UserBorrowBookForFine>> GetAllBooksAndUser(Expression<Func<BorrowedBook, bool>> predicate)
        {
            IQueryable<BorrowedBook> query =_context.BorrowedBooks.Where(predicate);

            var result= await query
                .Select(x=>new UserBorrowBookForFine(x.UserId,x.Id))
                .AsNoTracking()
                .ToListAsync();
            return result;
        }
    }
}
