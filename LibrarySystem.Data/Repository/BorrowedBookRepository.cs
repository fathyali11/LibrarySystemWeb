using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;

namespace LibrarySystem.Data.Repository
{
    public class BorrowedBookRepository(ApplicationDbContext context) : GenericRepository<BorrowedBook>(context), IBorrowedBookRepository
    {
    }
}
