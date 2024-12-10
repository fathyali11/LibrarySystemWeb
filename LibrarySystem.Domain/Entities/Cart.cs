namespace LibrarySystem.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId {  get; set; }=string.Empty;
        public DateTime CreatedOn { get; set; }=DateTime.UtcNow;
        public decimal TotalAmount {  get; set; }


        public ICollection<CartItem> CartItems { get; set; } = [];
        public ApplicationUser User { get; set; } = default!;

    }
}
