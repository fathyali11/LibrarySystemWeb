using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repository
{
    public class BorrowedBookRepository(ApplicationDbContext context) : GenericRepository<BorrowedBook>(context), IBorrowedBookRepository
    {
        private readonly ApplicationDbContext _context=context;

        public async Task RemoveAsync(string userId,CancellationToken cancellationToken=default)
        {
            await _context.BorrowedBooks
                .Where(x=>x.UserId == userId)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
