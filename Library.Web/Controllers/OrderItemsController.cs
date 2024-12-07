using System.Security.Claims;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Services.Services.OrderItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderItemsController(IOrderItemServices orderItemServices) : ControllerBase
    {
        private readonly IOrderItemServices _orderItemServices = orderItemServices;
        [HttpPut("plus-{id}")]
        public async Task<IActionResult> Plus(int id,CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result=await _orderItemServices.PlusAsync(id,userId, cancellationToken);
            return result.Match<IActionResult>(
                response=>Ok(),
                error=>error.ToProblem()
                );
        }
        [HttpPut("minus-{id}")]
        public async Task<IActionResult> Minus(int id, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _orderItemServices.MinusAsync(id,userId, cancellationToken);
            return Ok();
        }
        [HttpDelete("delete-{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _orderItemServices.RemoveAsync(id,userId, cancellationToken);
            return Ok();
        }
    }
}
