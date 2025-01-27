namespace Library.Web.Controllers
{
    [Route("me")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting("token")]
    public class AccountsController(IAccountUserServices accountUserServices) : ControllerBase
    {
        private readonly IAccountUserServices _accountUserServices = accountUserServices;

        /// <summary>
        /// update profile
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("update")]
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
