namespace LibrarySystem.Domain.DTO.Items;
public record ItemResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public decimal TotalPrice { get; init; }
    public string Type { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;

    public ItemResponse() { }

    public ItemResponse(int id, string name, decimal price, int quantity, decimal totalPrice, string type, string imageUrl)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        TotalPrice = totalPrice;
        Type = type;
        ImageUrl = imageUrl;
    }
}
