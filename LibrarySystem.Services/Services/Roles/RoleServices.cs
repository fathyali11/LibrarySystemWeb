using LibrarySystem.Domain.DTO.Roles;

namespace LibrarySystem.Services.Services.Roles;
public class RoleServices(IUnitOfWork unitOfWork,
    RoleManager<ApplicationRole> roleManager):IRoleServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    public async Task<IEnumerable<RoleReponse>> GetAllRolesAsync(CancellationToken cancellationToken=default)
    {
        var roles=await _unitOfWork.RoleRepository.GetRoles(cancellationToken);
        return roles;
    }
    public async Task<IEnumerable<RoleWithPermissionsResponse>> GetAllRolesWithPermissionsAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _unitOfWork.RoleRepository.GetRolesWithPermissions(cancellationToken);
        return roles;
    }

}
