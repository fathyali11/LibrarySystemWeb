using LibrarySystem.Domain.DTO.Roles;

namespace LibrarySystem.Services.Services.Roles;
/// <include file='ExternalServicesDocs\RolesServicesDocs.xml' path='/docs/members[@name="roleServices"]/RoleServices'/>
public class RoleServices(IUnitOfWork unitOfWork,
    RoleManager<ApplicationRole> roleManager):IRoleServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    /// <include file='ExternalServicesDocs\RolesServicesDocs.xml' path='/docs/members[@name="roleServices"]/GetAllRolesAsync'/>
    public async Task<IEnumerable<RoleReponse>> GetAllRolesAsync(CancellationToken cancellationToken=default)
    {
        var roles=await _unitOfWork.RoleRepository.GetRoles(cancellationToken);
        return roles;
    }
    /// <include file='ExternalServicesDocs\RolesServicesDocs.xml' path='/docs/members[@name="roleServices"]/GetAllRolesWithPermissionsAsync'/>

    public async Task<IEnumerable<RoleWithPermissionsResponse>> GetAllRolesWithPermissionsAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _unitOfWork.RoleRepository.GetRolesWithPermissions(cancellationToken);
        return roles;
    }

}
