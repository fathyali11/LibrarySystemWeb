using LibrarySystem.Domain.DTO.Books;
namespace LibrarySystem.Domain.DTO.Author;
public record AuthorWithBooksResponse(
    int Id,
    string Name,
    string Biography,
    bool IsDeleted,
    ICollection<BookResponse>Books
    );