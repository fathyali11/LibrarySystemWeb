using System.Security.Claims;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.DTO.ApplicationUsers;
using LibrarySystem.Services.Services.AccountUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Library.Web.Controllers
{
    [Route("me")]
    [ApiController]
    [Authorize]
    public class AccountsController(IAccountUserServices accountUserServices) : ControllerBase
    {
        private readonly IAccountUserServices _accountUserServices = accountUserServices;
        [HttpPut("update")]
        [EndpointDescription("Update user profile")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(AccountUserRequest request,CancellationToken cancellationToken)
        {
            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _accountUserServices.UpdateAsync(userId!, request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(),
                error => error.ToProblem()
                );
        }

        [HttpGet("profile")]
        [EndpointDescription("Get user profile")]
        [ProducesResponseType(typeof(AccountUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _accountUserServices.GetAsync(userId!,cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => error.ToProblem()
                );
        }

        [HttpPost("change-password")]
        [EndpointDescription("Change user password")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var result = await _accountUserServices.ChangePasswordAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(),
                error => error.ToProblem()
                );

        }
    }
}
