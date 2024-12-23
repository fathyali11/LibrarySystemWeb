using LibrarySystem.Domain.Abstractions.ConstValues;

namespace LibrarySystem.Domain.Entities;
public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; }=string.Empty;
    public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = OrderStatuss.Pending;
    public string PaymentStatus { get; set; } = PaymentStatuss.Pending;
    public int CartId { get; set; } 

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email {  get; set; } = string.Empty;
    public string Phone { get; set; }= string.Empty;

    public string? PaymentIntentId { get; set; }
    public string? sessionId { get; set; }

    // Navigation properties
    public ICollection<OrderItem> OrderItems { get; set; } = [];
    public ApplicationUser User { get; set; } = default!;
}
