using System.Linq;
using System.Linq.Expressions;
using LibrarySystem.Data.Data;
using LibrarySystem.Domain.DTO.Fines;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repository;
public class FineRepository(ApplicationDbContext context): GenericRepository<Fine>(context),IFineRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IEnumerable<FineResponse>> GetAllWithUserAndBookAsync(Expression<Func<Fine, bool>> predicate)
    {
        var query = _context.Fines
            .Where(predicate)
            .Include(x => x.User)
            .AsSplitQuery()
            .Include(x => x.BorrowedBook)
            .ThenInclude(x => x.Book)
            .AsSplitQuery();

        var result = await query.GroupBy(key => key.UserId)
            .Select(x => new FineResponse(
                x.FirstOrDefault()!.User.FirstName,
                x.FirstOrDefault()!.User.LastName,
                x.FirstOrDefault()!.User.Email!,
                x.Where(f => f.UserId == x.Key).Select(f => f.BorrowedBook.Book.Title).ToList(),
                x.FirstOrDefault()!.BorrowedBook.DueDate,
                x.FirstOrDefault()!.Amount,
                x.Sum(x => x.TotalAmount)
            ))
            .AsNoTracking()
            .ToListAsync();
        return result;
    }
    public async Task PayingOne(string userId, int borrowedBookId, CancellationToken cancellationToken = default)
    {
        await _context.Fines
            .Where(x=>x.UserId == userId && x.BorrowBookId == borrowedBookId)
            .ExecuteUpdateAsync(x => x.SetProperty(f => f.IsPaid,true), cancellationToken);
    }

}
