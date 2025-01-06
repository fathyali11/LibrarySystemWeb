using System.Security.Claims;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.ConstValues.DefaultValues;
using LibrarySystem.Domain.DTO.CartItems;
using LibrarySystem.Services.CustomAuthorization;
using LibrarySystem.Services.Services.CartItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CartItemsController(ICartItemServices cartItemServices) : ControllerBase
{
    private readonly ICartItemServices _cartItemServices = cartItemServices;

    [HttpPost("")]
    [HasPermission(MemberPermissions.OperationOnCart)]
    [EndpointDescription("Add new item to cart")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] CartItemRequest request,CancellationToken cancellationToken)
    {
        var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _cartItemServices.AddOrderToCartAsync(userId!, request, cancellationToken);
        return result.Match<IActionResult>(
            response=>Created(),
            error=>error.ToProblem()
            );
    }

    [HttpPut("plus-{id}")]
    [HasPermission(MemberPermissions.OperationOnCart)]
    [EndpointDescription("Plus item in cart")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
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
    [EndpointDescription("Minus item in cart")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
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
    [EndpointDescription("Remove item from cart")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
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
