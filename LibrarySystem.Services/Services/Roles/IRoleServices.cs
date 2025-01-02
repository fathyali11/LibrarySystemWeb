using LibrarySystem.Domain.DTO.Roles;

namespace LibrarySystem.Services.Services.Roles;
public interface IRoleServices
{
    Task<IEnumerable<RoleReponse>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<RoleWithPermissionsResponse>> GetAllRolesWithPermissionsAsync(CancellationToken cancellationToken = default);
}
