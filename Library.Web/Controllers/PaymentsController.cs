using System.Security.Claims;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.DTO.Payments;
using LibrarySystem.Services.Services.Payments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentsController(IPaymentServices paymentServices) : ControllerBase
{
    private readonly IPaymentServices _paymentServices = paymentServices;
    [HttpPost("create-checkout")]
    public async Task<IActionResult> CreateCheckout([FromBody] PaymentOrderRequest request,CancellationToken cancellationToken)
    {
        var sessionId = await _paymentServices.CreateCheckoutSessionAsync(request,cancellationToken);
        return Ok(new { sessionId });
    }
    [HttpPut("confirm-order-{orderId}")]
    public async Task<IActionResult> ConfirmOrder([FromRoute] int orderId,CancellationToken cancellationToken)
    {
        var result = await _paymentServices.ConfirmOrderPaymentStatus( orderId,cancellationToken);
        return result.Match<IActionResult>(
            response=>Ok(response),
            error=>error.ToProblem()
            );
    }
    [HttpPost("refund-order-{orderId}")]
    public async Task<IActionResult> RefundOrder([FromRoute] int orderId,CancellationToken cancellationToken)
    {
        var result = await _paymentServices.RefundPaymentStatus(orderId,cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
}
