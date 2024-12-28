namespace LibrarySystem.Domain.Entities
{
    public class Fine
    {
        public int Id { get; set; }
        public decimal Amount { get; set; } = 50;
        public decimal TotalAmount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UserId { get; set; } = string.Empty;
        public int BorrowBookId { get; set; }
        public bool IsPaid { get; set; } = false;

        public ApplicationUser User { get; set; } = default!;
        public BorrowedBook BorrowedBook { get; set; } = default!;

    }
}
