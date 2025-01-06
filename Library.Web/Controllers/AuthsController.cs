using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.DTO.ApplicationUsers;
using LibrarySystem.Services.Services.AuthUsers;
using LibrarySystem.Services.Services.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController(IAuthServices authServices,
        ITokenServices tokenServices) : ControllerBase
    {
        private readonly IAuthServices _authServices = authServices;
        private readonly ITokenServices _tokenServices = tokenServices;

        [HttpPost("register")]
        [EndpointDescription("Register new user")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegistersRequest request,CancellationToken cancellationToken)
        {
            var result = await _authServices.RegisterAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => error.ToProblem()
                );

        }
        [HttpPost("login")]
        [EndpointDescription("Login user")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginsRequest request, CancellationToken cancellationToken)
        {
            var result = await _authServices.LoginAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => error.ToProblem()
                );

        }
        [HttpPost("refresh-token")]
        [EndpointDescription("Refresh token")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _tokenServices.RefreshTokenAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => error.ToProblem()
                );

        }

        [HttpPut("confirm-email")]
        [EndpointDescription("Confirm email")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var result = await _authServices.ConfirmEmailAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(),
                error => error.ToProblem()
                );

        }

        [HttpPut("resend-confirm-email")]
        [EndpointDescription("Resend confirm email")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResendConfirmEmail(ResendConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var result = await _authServices.ResendConfirmEmailAsync(request, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(),
                error => error.ToProblem()
                );

        }

        [HttpPost("forget-password")]
        [EndpointDescription("Forget password")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequest request, CancellationToken cancellationToken)
        {
            var result=await _authServices.ForgetPasswordAsync(request,cancellationToken);
            return result.Match<IActionResult>(
                response=>Ok(),
                error=> error.ToProblem()
                );
        }

        [HttpPost("reset-password")]
        [EndpointDescription("Reset password")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
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
