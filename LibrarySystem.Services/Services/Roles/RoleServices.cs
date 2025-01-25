using LibrarySystem.Domain.DTO.Roles;
using Org.BouncyCastle.Tsp;

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
    public async Task<OneOf<RoleReponse,Error>> AddRoleASync(RoleRequest request,CancellationToken cancellationToken=default)
    {
        var roleIsExist = await _roleManager.Roles
            .AnyAsync(x => string.Equals(x.Name!.ToLower(), request.Name.ToLower()));
            
        if (roleIsExist)
            return RoleErrors.Exist;

        var role = new ApplicationRole
        {
            Name = request.Name
        };
        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return new Error(error.Code,error.Description,StatusCodes.Status400BadRequest);
        }
        return new RoleReponse(role.Id, role.Name);
    }
    public async Task<OneOf<RoleWithPermissionsResponse, Error>> AddPermissionToRoleAsync(string roleId, PermissionsRequest request, CancellationToken cancellationToken = default)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is null)
            return RoleErrors.NotExist;

        var permissionsFromDb=await _unitOfWork.RoleRepository.GetPermissions(roleId, cancellationToken);
        var permissions = request.Permissions.Except(permissionsFromDb).ToList();

        var claims =permissions.Select(x => new IdentityRoleClaim<string>
        {
            RoleId = role.Id,
            ClaimValue = x,
        }).ToList();
        await _unitOfWork.RoleRepository.AddClaims(claims, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        return new RoleWithPermissionsResponse(roleId, role.Name!, request.Permissions.ToList());
    }
    public async Task<OneOf<RoleReponse, Error>> UpdateRoleAsync(string roleId, RoleRequest request, CancellationToken cancellationToken = default)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is null)
            return RoleErrors.NotExist;

        var roleIsExist = await _roleManager.Roles
            .AnyAsync(x => string.Equals(x.Name!.ToLower(), request.Name.ToLower()));

        if (roleIsExist)
            return RoleErrors.Exist;

        role.Name = request.Name;
        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return new Error(error.Code, error.Description, StatusCodes.Status400BadRequest);
        }
        return new RoleReponse(role.Id, role.Name);
    }
    public async Task<OneOf<RoleWithPermissionsResponse,Error>> AddRoleWithPermissionsAsync(RoleWithPermissionsRequest request, CancellationToken cancellationToken= default)
    {
        var roleIsExist = await _roleManager.Roles
            .AnyAsync(x => string.Equals(x.Name!.ToLower(), request.Name.ToLower()));
        if (roleIsExist)
            return RoleErrors.Exist;
        var role = new ApplicationRole
        {
            Name = request.Name
        };
        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return new Error(error.Code, error.Description, StatusCodes.Status400BadRequest);
        }
        var permissions = request.Permissions.Select(x => new IdentityRoleClaim<string>
        {
            RoleId = role.Id,
            ClaimValue = x,
        }).ToList();
        
        await _unitOfWork.RoleRepository.AddClaims(permissions, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        return new RoleWithPermissionsResponse(role.Id, role.Name, permissions.Select(x =>x.ClaimValue!).ToList());
    }
    
    public async Task<OneOf<bool,Error>> RemoveRoleAsync(string roleId, CancellationToken cancellationToken = default)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is null)
            return RoleErrors.NotExist;
        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return new Error(error.Code, error.Description, StatusCodes.Status400BadRequest);
        }
        await _unitOfWork.RoleRepository.RemoveClaims(roleId, cancellationToken);
        return true;
    }
}
