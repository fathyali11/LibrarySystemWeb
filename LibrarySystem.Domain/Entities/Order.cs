namespace LibrarySystem.Domain.Entities;
public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; } 
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<OrderItem> OrderItems { get; set; } = [];
    public ApplicationUser User { get; set; } = default!;  // Assuming User entity exists
}
