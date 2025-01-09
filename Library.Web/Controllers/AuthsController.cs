using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.DTO.ApplicationUsers;
using LibrarySystem.Services.Services.AuthUsers;
using LibrarySystem.Services.Services.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Library.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("IdLimiter")]
    public class AuthsController(IAuthServices authServices,
        ITokenServices tokenServices) : ControllerBase
    {
        private readonly IAuthServices _authServices = authServices;
        private readonly ITokenServices _tokenServices = tokenServices;

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
            var result = await _tokenServices.RefreshTokenAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
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
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequest request, CancellationToken cancellationToken)
        {
            var result=await _authServices.ForgetPasswordAsync(request,cancellationToken);
            return result.Match<IActionResult>(
                response=>Ok(),
                error=> error.ToProblem()
                );
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var result = await _authServices.ResetPasswordAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(),
                error => error.ToProblem()
                );
        }
    }
}
