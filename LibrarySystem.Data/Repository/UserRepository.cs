
using LibrarySystem.Data.Data;
using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using LibrarySystem.Domain.DTO.ApplicationUsers;
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
        public async Task<List<UserResponse>> GetAll(CancellationToken cancellationToken = default)
        {
            var query = (
                from user in _context.Users
                join userRole in _context.UserRoles
                on user.Id equals userRole.UserId
                join role in _context.Roles
                on userRole.RoleId equals role.Id
                where user.Id!= ManagerUser.Id
                select new UserResponse
                (
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email!,
                    user.UserName!,
                    user.Address,
                    user.PhoneNumber!,
                    role.Name!,
                    user.IsActive
                )
                ).AsSplitQuery()
                .AsQueryable();

            return await query.ToListAsync(cancellationToken);
        }
    }
}
