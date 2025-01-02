using LibrarySystem.Data.Data;
using LibrarySystem.Domain.DTO.Roles;
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
    public async Task<IEnumerable<RoleReponse>> GetRoles(CancellationToken cancellationToken = default)
    {
        var roles = _context.Roles.Select(r =>new RoleReponse(r.Id,r.Name!)).AsQueryable();
        return await roles.ToListAsync(cancellationToken);
    }
    public async Task<IEnumerable<RoleWithPermissionsResponse>> GetRolesWithPermissions(CancellationToken cancellationToken = default)
    {
        var query=(from role in _context.Roles
                  join roleClaim in _context.RoleClaims
                  on role.Id equals roleClaim.RoleId into roleClaims
                   select new RoleWithPermissionsResponse(role.Id, role.Name!, roleClaims.Select(rc => rc.ClaimValue!))
                    ).AsQueryable();


        return await query.ToListAsync(cancellationToken);
    }
}
