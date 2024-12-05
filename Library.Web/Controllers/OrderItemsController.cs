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
            var result=await _orderItemServices.PlusAsync(id,cancellationToken);
            return Ok();
        }
        [HttpPut("minus-{id}")]
        public async Task<IActionResult> Minus(int id, CancellationToken cancellationToken)
        {
            var result = await _orderItemServices.MinusAsync(id, cancellationToken);
            return Ok();
        }
        [HttpDelete("delete-{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _orderItemServices.RemoveAsync(id, cancellationToken);
            return Ok();
        }
    }
}
