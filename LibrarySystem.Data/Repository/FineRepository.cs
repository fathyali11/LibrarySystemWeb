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
    public async Task<IEnumerable<FineResponse>> GetAllWithUserAndBookAsync(Expression<Func<Fine,bool>> predicate)
    {
        return await _context.Fines
            .Where(predicate)
            .Include(x => x.User)
            .AsSplitQuery()
            .Include(x => x.BorrowedBook)
            .ThenInclude(x => x.Book)
            .AsSplitQuery()
            .Select(x => new FineResponse(
                x.User.FirstName,
                x.User.LastName,
                x.User.Email!,
                x.BorrowedBook.Book.Title,
                x.BorrowedBook.DueDate,
                x.Amount,
                x.TotalAmount
            ))
            .AsNoTracking()
            .ToListAsync();

    }
}
