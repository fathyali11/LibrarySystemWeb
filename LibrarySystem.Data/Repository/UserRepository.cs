
using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repository
{
    public class UserRepository(ApplicationDbContext context) : GenericRepository<ApplicationUser>(context), IUserRepository
    {
        private readonly ApplicationDbContext _context=context;
        public async Task<bool> IsExistAsync(string userName,string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email || x.UserName == userName);
        }
    }
}
