namespace LibrarySystem.Domain.DTO.OrderItems;
public record OrderItemResponse
{
    public int Id { get; init; }
    public string BookName { get; init; } = string.Empty;
    public decimal BookPrice { get; init; }
    public int Quantity { get; init; }

    // Parameterless constructor
    public OrderItemResponse() { }

    // Primary constructor
    public OrderItemResponse(int id, string bookName, decimal bookPrice, int quantity)
    {
        Id = id;
        BookName = bookName;
        BookPrice = bookPrice;
        Quantity = quantity;
    }
}


