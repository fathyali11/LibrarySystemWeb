using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using LibrarySystem.Domain.DTO.Roles;
using LibrarySystem.Services.CustomAuthorization;
using LibrarySystem.Services.Services.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("token")]
public class RolesController(IRoleServices roleServices) : ControllerBase
{
    private readonly IRoleServices _roleServices = roleServices;
    [HttpGet("")]
    [HasPermission(ManagerPermissions.GetRoles)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var roles = await _roleServices.GetAllRolesAsync(cancellationToken);
        return Ok(roles);
    }
    [HttpGet("permissions")]
    [HasPermission(ManagerPermissions.GetRoles)]
    public async Task<IActionResult> GetAllWithPermissions(CancellationToken cancellationToken = default)
    {
        var roles = await _roleServices.GetAllRolesWithPermissionsAsync(cancellationToken);
        return Ok(roles);
    }
    [HttpPost("")]
    [HasPermission(ManagerPermissions.CreateRole)]
    public async Task<IActionResult> AddRole(RoleRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _roleServices.AddRoleASync(request, cancellationToken);
        return result.Match<IActionResult>(
            role => Ok(role),
            error => error.ToProblem()
        );
    }
    [HttpPost("{roleId}/permissions")]
    [HasPermission(ManagerPermissions.CreateRole)]
    public async Task<IActionResult> AddPermissionToRole(string roleId, PermissionsRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _roleServices.AddPermissionToRoleAsync(roleId, request, cancellationToken);
        return result.Match<IActionResult>(
            role => Ok(role),
            error => error.ToProblem()
        );
    }
    [HttpPut("{roleId}")]
    [HasPermission(ManagerPermissions.UpdateRole)]
    public async Task<IActionResult> UpdateRole(string roleId, RoleRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _roleServices.UpdateRoleAsync(roleId, request, cancellationToken);
        return result.Match<IActionResult>(
            role => Ok(role),
            error => error.ToProblem()
        );
    }
    [HttpPost("permissions")]
    [HasPermission(ManagerPermissions.CreateRole)]
    public async Task<IActionResult> AddRoleWithPermissions(RoleWithPermissionsRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _roleServices.AddRoleWithPermissionsAsync(request, cancellationToken);
        return result.Match<IActionResult>(
            role => Ok(role),
            error => error.ToProblem()
        );
    }
    [HttpDelete("{roleId}")]
    [HasPermission(ManagerPermissions.DeleteRole)]
    public async Task<IActionResult> RemoveRole(string roleId, CancellationToken cancellationToken = default)
    {
        var result = await _roleServices.RemoveRoleAsync(roleId, cancellationToken);
        return result.Match<IActionResult>(
            _ => Ok(),
            error => error.ToProblem()
        );
    }
}
