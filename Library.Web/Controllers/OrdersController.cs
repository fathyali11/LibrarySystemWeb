using System.Security.Claims;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using LibrarySystem.Domain.DTO.Orders;
using LibrarySystem.Services.CustomAuthorization;
using LibrarySystem.Services.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderServices orderServices) : ControllerBase
{
    private readonly IOrderServices _orderServices = orderServices;

    [HttpPost("{cartId}")]
    [HasPermission(MemberPermissions.CreateOrder)]
    [EndpointDescription("Make order")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
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
    [EndpointDescription("Get all orders")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
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
    [HasPermission(MemberPermissions.CancelOrder)]
    [EndpointDescription("Cancel order")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cancel([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _orderServices.CancelOrderAsync(id, cancellationToken);

        return result.Match<IActionResult>(
            Response => NoContent(),
            error => error.ToProblem()
            );
    }

}
