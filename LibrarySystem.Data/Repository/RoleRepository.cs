using LibrarySystem.Data.Data;
using LibrarySystem.Domain.DTO.Roles;
using LibrarySystem.Domain.IRepository;
using Microsoft.AspNetCore.Identity;
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
    public async Task<IEnumerable<string?>> GetPermissions(string roleId, CancellationToken cancellationToken = default)
    {
        var permissions = _context.RoleClaims
            .Where(rc => rc.RoleId == roleId)
            .Select(rc => rc.ClaimValue)
            .AsQueryable();
        return await permissions.ToListAsync(cancellationToken);
    }
    public async Task AddClaims(IEnumerable<IdentityRoleClaim<string>> claims,CancellationToken cancellationToken)
    {
        await _context.RoleClaims.AddRangeAsync(claims, cancellationToken);
    }
    public async Task RemoveClaims(string roleId, CancellationToken cancellationToken)
    {
        var claims = await _context.RoleClaims.Where(rc => rc.RoleId == roleId).ToListAsync(cancellationToken);
        _context.RoleClaims.RemoveRange(claims);
    }
}
