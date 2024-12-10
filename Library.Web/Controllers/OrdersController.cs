//using System.Security.Claims;
//using LibrarySystem.Domain.Abstractions;
//using LibrarySystem.Domain.DTO.Orders;
//using LibrarySystem.Services.Services.Orders;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Library.Web.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class OrdersController(IOrderServices orderServices) : ControllerBase
//    {
//        private readonly IOrderServices _orderServices = orderServices;

//        [HttpPost("")]
//        public async Task<IActionResult> Add(OrderItemRequest request,CancellationToken cancellationToken)
//        {
//            string userId=User.FindFirstValue(ClaimTypes.NameIdentifier)!;
//            var result = await _orderServices.AddOrderAsync(userId,request, cancellationToken);

//            return result.Match<IActionResult>(
//                Response=>Ok(Response),
//                error=>error.ToProblem()
//                );
//        }

//        [HttpGet("")]
//        public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
//        {
//            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
//            var result = await _orderServices.GetAllOrdersAsync(userId, cancellationToken);

//            return result.Match<IActionResult>(
//                Response => Ok(Response),
//                error => error.ToProblem()
//                );
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
//        {
//            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier)!;
//            var result = await _orderServices.RemoveOrdersAsync(id,userId, cancellationToken);
//            return result ?Ok():BadRequest();
//        }

//        [HttpPut("confirm-order-{id}")]
//        public async Task<IActionResult> ConfirmOrder(int id, CancellationToken cancellationToken)
//        {
//            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
//            var result = await _orderServices.ConfirmOrderAsync(id,userId, cancellationToken);
//            return result.Match<IActionResult>(
//                response=>Ok(),
//                error => error.ToProblem()
//                );
//        }
//        [HttpPut("remove-confirm-order-{id}")]
//        public async Task<IActionResult> RemoveConfirmOrder(int id, CancellationToken cancellationToken)
//        {
//            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
//            var result = await _orderServices.RemoveConfirmOrderAsync(id, userId, cancellationToken);
//            return result.Match<IActionResult>(
//                response => Ok(),
//                error => error.ToProblem()
//                );
//        }
//    }
//}
