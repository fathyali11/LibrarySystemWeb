using LibrarySystem.Domain.DTO.Carts;

namespace LibrarySystem.Domain.DTO.Orders;
public record OrderResponse:CartResponse
{
    public string FirstName { get; init; }=string.Empty;
    public string LastName { get; init; }= string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Email {  get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;

}


