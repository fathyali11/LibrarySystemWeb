using System.Linq.Expressions;
using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repository
{
    public class BorrowedBookRepository(ApplicationDbContext context) : GenericRepository<BorrowedBook>(context), IBorrowedBookRepository
    {
        private readonly ApplicationDbContext _context=context;
        public async Task<List<BorrowedBook>> GetAllWithUserAndBook(Expression<Func<BorrowedBook, bool>> predicate)
        {
            var result =await _context.BorrowedBooks
                .Where(predicate)
                .Include(x => x.Book)
                .Include(x => x.User)
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
