using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using LibrarySystem.Domain.DTO.Roles;
using LibrarySystem.Services.CustomAuthorization;
using LibrarySystem.Services.Services.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RolesController(IRoleServices roleServices) : ControllerBase
{
    private readonly IRoleServices _roleServices = roleServices;
    [HttpGet("")]
    [HasPermission(ManagerPermissions.GetRoles)]
    [EndpointDescription("Get all roles")]
    [ProducesResponseType(typeof(IEnumerable<RoleReponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var roles = await _roleServices.GetAllRolesAsync(cancellationToken);
        return Ok(roles);
    }
    [HttpGet("permissions")]
    [HasPermission(ManagerPermissions.GetRoles)]
    [EndpointDescription("Get all roles with permissions")]
    [ProducesResponseType(typeof(IEnumerable<RoleWithPermissionsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllWithPermissions(CancellationToken cancellationToken = default)
    {
        var roles = await _roleServices.GetAllRolesWithPermissionsAsync(cancellationToken);
        return Ok(roles);
    }
}
