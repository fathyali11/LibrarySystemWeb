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
}
