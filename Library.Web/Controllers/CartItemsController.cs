﻿
namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("token")]
public class CartItemsController(ICartItemServices cartItemServices) : ControllerBase
{
    private readonly ICartItemServices _cartItemServices = cartItemServices;

    [HttpPost("")]
    [HasPermission(MemberPermissions.OperationOnCart)]
    public async Task<IActionResult> Add([FromBody] CartItemRequest request,CancellationToken cancellationToken)
    {
        var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _cartItemServices.AddOrderToCartAsync(userId!, request, cancellationToken);
        return result.Match<IActionResult>(
            response=>Ok(response),
            error=>error.ToProblem()
            );
    }

    [HttpPut("plus-{id}")]
    [HasPermission(MemberPermissions.OperationOnCart)]
    public async Task<IActionResult> Plus([FromRoute] int id, CancellationToken cancellationToken)
    {
        var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _cartItemServices.PlusAsync(userId!, id, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(),
            error => error.ToProblem()
            );
    }
    [HttpPut("minus-{id}")]
    [HasPermission(MemberPermissions.OperationOnCart)]
    public async Task<IActionResult> Minus([FromRoute] int id, CancellationToken cancellationToken)
    {
        var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _cartItemServices.MinusAsync(userId!, id, cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(),
            error => error.ToProblem()
            );
    }
    [HttpDelete("{id}")]
    [HasPermission(MemberPermissions.OperationOnCart)]
    public async Task<IActionResult> Remove([FromRoute] int id, CancellationToken cancellationToken)
    {
        var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _cartItemServices.RemoveAsync(userId!, id, cancellationToken);
        return result.Match<IActionResult>(
            response => NoContent(),
            error => error.ToProblem()
            );
    }
}
