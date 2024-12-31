using LibrarySystem.Data.Data;
using LibrarySystem.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repository;
public class RoleRepository(ApplicationDbContext context) : IRoleRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IEnumerable<string>> GetPermissions(IEnumerable<string> roles, CancellationToken cancellationToken = default)
    {
        var permissions = ( 
            from role in _context.Roles
            join roleClaim in _context.RoleClaims
            on role.Id equals roleClaim.RoleId
            where roles.Contains(role.Name)
            select roleClaim.ClaimValue
            ).Distinct().AsQueryable();

        return await permissions.ToListAsync(cancellationToken);
    }
}
