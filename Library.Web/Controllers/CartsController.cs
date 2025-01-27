namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("token")]
public class CartsController(ICartServices cartServices) : ControllerBase
{
    private readonly ICartServices _cartServices=cartServices;

    [HttpGet("{id}")]
    [HasPermission(MemberPermissions.GetCarts)]
    public async Task<IActionResult>  Get(int id,CancellationToken cancellationToken)
    {
        var result=await _cartServices.GetCartAsync(id,cancellationToken);
        return result.Match<IActionResult>(
            response=>Ok(response),
            error=>error.ToProblem()
            );
    }
    [HttpPut("{id}")]
    [HasPermission(MemberPermissions.ClearCarts)]
    public async Task<IActionResult> Clear(int id, CancellationToken cancellationToken)
    {
        var result = await _cartServices.ClearCartAsync(id, cancellationToken);
        return result.Match<IActionResult>(
            response => NoContent(),
            error => error.ToProblem()
            );
    }
}
