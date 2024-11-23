using LibrarySystem.Domain.DTO.Books;

namespace LibrarySystem.Domain.DTO.Categories;
public record CategoryWithBooksResponse(
    int Id,
    string Name,
    string Description, 
    bool IsDeleted,
    ICollection<BookResponse> Books
    );

