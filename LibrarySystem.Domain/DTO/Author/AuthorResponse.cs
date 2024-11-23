namespace LibrarySystem.Domain.DTO.Author;
public record AuthorResponse(
    int Id,
    string Name,
    string Biography,
    bool IsDeleted
    );
