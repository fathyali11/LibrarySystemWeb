namespace LibrarySystem.Domain.Entities;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; } 
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }  // Price for this book (can be different for borrowing or buying)

    // Navigation properties
    public BorrowOrBuyBook Book { get; set; } = default!;
    public Order Order { get; set; }= default!;
}
