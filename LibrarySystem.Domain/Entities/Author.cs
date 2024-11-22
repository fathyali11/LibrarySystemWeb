namespace LibrarySystem.Domain.Entities;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }=string.Empty;
    public string Biography { get; set; }= string.Empty;

    // Navigation property
    public ICollection<Book> Books { get; set; } = []; // An author must has at least one book
}
