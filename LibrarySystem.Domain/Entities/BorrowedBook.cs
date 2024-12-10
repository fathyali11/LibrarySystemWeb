namespace LibrarySystem.Domain.Entities;

public class BorrowedBook
{
    public int Id { get; set; }
    public DateTime BorrowDate { get; set; }=DateTime.Now;
    public DateTime DueDate {  get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned=>ReturnDate.HasValue;
    public string UserId { get; set; } = string.Empty;
    public int BookId { get; set; }
    public ApplicationUser User { get; set; } = default!;
    // Navigation properties
    public Book Book { get; set; }= default!;
    
}
