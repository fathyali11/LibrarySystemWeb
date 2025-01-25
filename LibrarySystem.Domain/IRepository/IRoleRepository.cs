using LibrarySystem.Domain.DTO.Roles;
using Microsoft.AspNetCore.Identity;

namespace LibrarySystem.Domain.IRepository;
public interface IRoleRepository
{
    Task<IEnumerable<string>> GetPermissions(IEnumerable<string> roles, CancellationToken cancellationToken=default);
    Task<IEnumerable<RoleReponse>> GetRoles(CancellationToken cancellationToken = default);
    Task<IEnumerable<RoleWithPermissionsResponse>> GetRolesWithPermissions(CancellationToken cancellationToken = default);
    Task<IEnumerable<string?>> GetPermissions(string roleId, CancellationToken cancellationToken = default);
    Task AddClaims(IEnumerable<IdentityRoleClaim<string>> claims, CancellationToken cancellationToken);
    Task RemoveClaims(string roleId, CancellationToken cancellationToken);
}
