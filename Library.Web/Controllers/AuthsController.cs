using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.DTO.ApplicationUsers;
using LibrarySystem.Services.Services.AuthUsers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController(IAuthServices authServices) : ControllerBase
    {
        private readonly IAuthServices _authServices = authServices;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistersRequest request,CancellationToken cancellationToken)
        {
            var result = await _authServices.RegisterAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => error.ToProblem()
                );

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginsRequest request, CancellationToken cancellationToken)
        {
            var result = await _authServices.LoginAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => error.ToProblem()
                );

        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _authServices.RefreshTokenAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => error.ToProblem()
                );

        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var result = await _authServices.ChangePasswordAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(),
                error => error.ToProblem()
                );

        }

        [HttpPut("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var result = await _authServices.ConfirmEmailAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(),
                error => error.ToProblem()
                );

        }

        [HttpPut("resend-confirm-email")]
        public async Task<IActionResult> ResendConfirmEmail(ResendConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var result = await _authServices.ResendConfirmEmailAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(),
                error => error.ToProblem()
                );

        }

    }
}
