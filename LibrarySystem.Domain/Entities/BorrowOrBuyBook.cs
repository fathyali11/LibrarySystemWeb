namespace LibrarySystem.Domain.Entities;

public class BorrowOrBuyBook
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string ?Status { get; set; }
    public bool IsBorrow {  get; set; }=true;//assume user borrow book 

    // Navigation properties
    public Book Book { get; set; }= default!;
}
