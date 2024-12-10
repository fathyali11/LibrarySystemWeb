namespace LibrarySystem.Domain.Entities
{
    public class CartItem:ItemBase
    {
        public int CartId { get; set; }
        public Cart Cart { get; set; } = default!;
    }
}
