using LibrarySystem.Domain.DTO.Roles;

namespace LibrarySystem.Services.Services.Roles;
public interface IRoleServices
{
    Task<IEnumerable<RoleReponse>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<RoleWithPermissionsResponse>> GetAllRolesWithPermissionsAsync(CancellationToken cancellationToken = default);
    Task<OneOf<RoleReponse, Error>> AddRoleASync(RoleRequest request, CancellationToken cancellationToken = default);
    Task<OneOf<RoleWithPermissionsResponse, Error>> AddPermissionToRoleAsync(string roleId, PermissionsRequest request, CancellationToken cancellationToken = default);
    Task<OneOf<RoleReponse, Error>> UpdateRoleAsync(string roleId, RoleRequest request, CancellationToken cancellationToken = default);
    Task<OneOf<RoleWithPermissionsResponse, Error>> AddRoleWithPermissionsAsync(RoleWithPermissionsRequest request, CancellationToken cancellationToken = default);
    Task<OneOf<bool, Error>> RemoveRoleAsync(string roleId, CancellationToken cancellationToken = default);



}
