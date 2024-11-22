namespace LibrarySystem.Domain.Entities;
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }=string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal PriceForBuy { get; set; }
    public decimal PriceForBorrow { get; set; }
    public DateTime PublishedDate { get; set; }
    public bool IsAvailable => Quantity > 0;
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }


    // Navigation properties
    public Category Category { get; set; } = default!;
    public Author Author { get; set; } = default!;
}
