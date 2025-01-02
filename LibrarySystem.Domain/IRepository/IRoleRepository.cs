using LibrarySystem.Domain.DTO.Roles;

namespace LibrarySystem.Domain.IRepository;
public interface IRoleRepository
{
    Task<IEnumerable<string>> GetPermissions(IEnumerable<string> roles, CancellationToken cancellationToken=default);
    Task<IEnumerable<RoleReponse>> GetRoles(CancellationToken cancellationToken = default);
    Task<IEnumerable<RoleWithPermissionsResponse>> GetRolesWithPermissions(CancellationToken cancellationToken = default);
}
