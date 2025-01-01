using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using LibrarySystem.Domain.DTO.ApplicationUsers;
using LibrarySystem.Services.CustomAuthorization;
using LibrarySystem.Services.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserServices userServices) : ControllerBase
{
    private readonly IUserServices _userServices= userServices;
    [HttpPost("")]
    [HasPermission(ManagerPermissions.CreateUser)]
    public async Task<IActionResult> Add(CreateOrUpdateUserRequest request,CancellationToken cancellationToken)
    {
        var result = await _userServices.CreateUserAsync(request,cancellationToken);
        return result.Match<IActionResult>(
            user => Ok(user),
            error => error.ToProblem()
        );
    }
    [HttpPut("{userId}")]
    [HasPermission(ManagerPermissions.UpdateUser)]
    public async Task<IActionResult> Update(string userId, CreateOrUpdateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userServices.UpdateUserAsync(userId, request, cancellationToken);
        return result.Match<IActionResult>(
            user => Ok(user),
            error => error.ToProblem()
        );
    }
    [HttpGet("{userId}")]
    [HasPermission(ManagerPermissions.GetUser)]
    public async Task<IActionResult> Get(string userId, CancellationToken cancellationToken)
    {
        var result = await _userServices.GetUserByAsync(userId, cancellationToken);
        return result.Match<IActionResult>(
            user => Ok(user),
            error => error.ToProblem()
        );
    }
    [HttpGet("")]
    [HasPermission(ManagerPermissions.GetUser)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _userServices.GetAllUsersAsync(cancellationToken);
        return result.Match<IActionResult>(
            users => Ok(users),
            error => error.ToProblem()
        );
    }
    [HttpPut("{userId}/activation")]
    [HasPermission(ManagerPermissions.UpdateUser)]
    public async Task<IActionResult> ChangeActivation(string userId, CancellationToken cancellationToken)
    {
        var result = await _userServices.ChangeUserActivationAsync(userId, cancellationToken);
        return result.Match<IActionResult>(
            _ => Ok(),
            error => error.ToProblem()
        );
    }

    [HttpPut("{userId}/role")]
    [HasPermission(ManagerPermissions.UpdateUser)]
    public async Task<IActionResult> ChangeRole(string userId, ChangeUserRoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _userServices.ChangeRoleOfUserAsync(userId, request, cancellationToken);
        return result.Match<IActionResult>(
            _ => Ok(),
            error => error.ToProblem()
        );
    }

}
