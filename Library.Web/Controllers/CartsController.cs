using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Services.Services.Carts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CartsController(ICartServices cartServices) : ControllerBase
{
    private readonly ICartServices _cartServices=cartServices;

    [HttpGet("{id}")]
    public async Task<IActionResult>  Get(int id,CancellationToken cancellationToken)
    {
        var result=await _cartServices.GetCartAsync(id,cancellationToken);
        return result.Match<IActionResult>(
            response=>Ok(response),
            error=>error.ToProblem()
            );
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Clear(int id, CancellationToken cancellationToken)
    {
        var result = await _cartServices.ClearCartAsync(id, cancellationToken);
        return result.Match<IActionResult>(
            response => NoContent(),
            error => error.ToProblem()
            );
    }
}
