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
        public async Task<List<BorrowBookNotificationResponse>> GetAllWithUserAndBook(Expression<Func<BorrowedBook, bool>> predicate)
        {
            var result =await _context.BorrowedBooks
                .Where(predicate)
                .Include(x => x.Book)
                .Include(x => x.User)
                .Select(x=>new BorrowBookNotificationResponse(
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Email!,
                    x.Book.Title,
                    x.DueDate
                    ))
                .ToListAsync();

            return result;
        }

        public async Task RemoveAsync(string userId,CancellationToken cancellationToken=default)
        {
            await _context.BorrowedBooks
                .Where(x=>x.UserId == userId)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
