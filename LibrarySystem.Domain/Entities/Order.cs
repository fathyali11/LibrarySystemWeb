namespace LibrarySystem.Domain.Entities;
public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; }=string.Empty;
    public DateTime OrderDate { get; set; }=DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    // Navigation properties
    public ICollection<OrderItem> OrderItems { get; set; } = [];
    public ApplicationUser User { get; set; } = default!;  // Assuming User entity exists
}
