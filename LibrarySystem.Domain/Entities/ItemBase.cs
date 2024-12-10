namespace LibrarySystem.Domain.Entities
{
    public class ItemBase
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }// Price for this book (can be different for borrowing or buying)
        public string OrderType {  get; set; }=string.Empty;

        public Book Book { get; set; } = default!;
    }
}
