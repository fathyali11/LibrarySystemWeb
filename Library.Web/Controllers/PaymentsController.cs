namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("token")]
public class PaymentsController(IPaymentServices paymentServices) : ControllerBase
{
    private readonly IPaymentServices _paymentServices = paymentServices;
    [HttpPost("create-checkout")]
    [HasPermission(MemberPermissions.CreatePayment)]
    public async Task<IActionResult> CreateCheckout([FromBody] PaymentOrderRequest request,CancellationToken cancellationToken)
    {
        var sessionId = await _paymentServices.CreateCheckoutSessionAsync(request,cancellationToken);
        return Ok(new { sessionId });
    }
    [HttpPut("confirm-order-{orderId}")]
    [HasPermission(ManagerPermissions.UpdatePayment)]

    public async Task<IActionResult> ConfirmOrder([FromRoute] int orderId,CancellationToken cancellationToken)
    {
        var result = await _paymentServices.ConfirmOrderPaymentStatus( orderId,cancellationToken);
        return result.Match<IActionResult>(
            response=>Ok(response),
            error=>error.ToProblem()
            );
    }
    [HttpPut("refund-order-{orderId}")]
    [HasPermission(ManagerPermissions.DeletePayment)]
    public async Task<IActionResult> RefundOrder([FromRoute] int orderId,CancellationToken cancellationToken)
    {
        var result = await _paymentServices.RefundPaymentStatus(orderId,cancellationToken);
        return result.Match<IActionResult>(
            response => Ok(response),
            error => error.ToProblem()
            );
    }
}
