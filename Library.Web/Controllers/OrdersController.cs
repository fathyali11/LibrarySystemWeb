﻿namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("token")]
public class OrdersController(IOrderServices orderServices) : ControllerBase
{
    private readonly IOrderServices _orderServices = orderServices;

    [HttpPost("{cartId}")]
    [HasPermission(MemberPermissions.CreateOrder)]
    public async Task<IActionResult> Add([FromRoute] int cartId, CancellationToken cancellationToken)
    {
        var result = await _orderServices.MakeOrderAsync(cartId, cancellationToken);

        return result.Match<IActionResult>(
            Response => Ok(Response),
            error => error.ToProblem()
            );
    }
    [HttpGet("")]
    [HasPermission(MemberPermissions.GetOrders)]
    public async Task<IActionResult> Get( CancellationToken cancellationToken)
    {
        var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _orderServices.GetOrderAsync(userId!, cancellationToken);

        return result.Match<IActionResult>(
            Response => Ok(Response),
            error => error.ToProblem()
            );
    }
    [HttpDelete("{id}")]
    [HasPermission(MemberPermissions.DeleteOrder)]
    public async Task<IActionResult> Cancel([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _orderServices.CancelOrderAsync(id, cancellationToken);

        return result.Match<IActionResult>(
            Response => NoContent(),
            error => error.ToProblem()
            );
    }

}
