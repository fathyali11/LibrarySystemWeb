using System.Security.Claims;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.DTO.Orders;
using LibrarySystem.Services.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController(IOrderServices orderServices) : ControllerBase
    {
        private readonly IOrderServices _orderServices = orderServices;

        [HttpPost("")]
        public async Task<IActionResult> Add(OrderRequest request,CancellationToken cancellationToken)
        {
            string userId=User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _orderServices.AddOrderAsync(userId,request, cancellationToken);

            return result.Match<IActionResult>(
                Response=>Ok(Response),
                error=>error.ToProblem()
                );
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _orderServices.GetAllOrdersAsync(userId, cancellationToken);

            return result.Match<IActionResult>(
                Response => Ok(Response),
                error => error.ToProblem()
                );
        }
    }
}
