namespace LibrarySystem.Domain.Entities;

public class BorrowedBook
{
    public int Id { get; set; }
    public DateTime BorrowDate { get; set; }=DateTime.Now;
    public DateTime DueDate {  get; set; }= DateTime.Now.AddDays(14);
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned=>ReturnDate.HasValue;
    public string UserId { get; set; } = string.Empty;
    public int BookId { get; set; }
    public int ?FineId { get; set; }
    public ApplicationUser User { get; set; } = default!;
    // Navigation properties
    public Book Book { get; set; }= default!;
    public Fine? Fine { get; set; }

}
