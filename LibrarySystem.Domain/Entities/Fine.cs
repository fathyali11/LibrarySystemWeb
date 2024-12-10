namespace LibrarySystem.Domain.Entities
{
    public class Fine
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UserId { get; set; } = string.Empty;
        public int BorrowBookId { get; set; }

        public ApplicationUser ApplicationUser { get; set; } = default!;
        public BorrowedBook BorrowedBook { get; set; } = default!;

    }
}
