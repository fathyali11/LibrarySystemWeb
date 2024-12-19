using System.Security.Claims;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Services.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrdersController(IOrderServices orderServices) : ControllerBase
    {
        private readonly IOrderServices _orderServices = orderServices;

        [HttpPost("{cartId}")]
        public async Task<IActionResult> Add([FromRoute] int cartId, CancellationToken cancellationToken)
        {
            var result = await _orderServices.MakeOrderAsync(cartId, cancellationToken);

            return result.Match<IActionResult>(
                Response => Ok(Response),
                error => error.ToProblem()
                );
        }
        [HttpGet("")]
        public async Task<IActionResult> Get( CancellationToken cancellationToken)
        {
            //var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = "cc127b55-2a31-468e-8e6b-daa58e5dca66";
            var result = await _orderServices.GetOrderAsync(userId!, cancellationToken);

            return result.Match<IActionResult>(
                Response => Ok(Response),
                error => error.ToProblem()
                );
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _orderServices.CancelOrderAsync(id, cancellationToken);

            return result.Match<IActionResult>(
                Response => NoContent(),
                error => error.ToProblem()
                );
        }

    }
}
