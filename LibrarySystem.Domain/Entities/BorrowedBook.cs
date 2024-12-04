namespace LibrarySystem.Domain.Entities;

public class BorrowedBook
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string ?Status { get; set; }
    public bool IsBorrow {  get; set; }
    public ApplicationUser User { get; set; } = default!;
    // Navigation properties
    public Book Book { get; set; }= default!;
    public string UserId {  get; set; }=string.Empty;
}
