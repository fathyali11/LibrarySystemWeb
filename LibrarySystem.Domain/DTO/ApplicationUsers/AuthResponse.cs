namespace LibrarySystem.Domain.DTO.ApplicationUsers;
public record AuthResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string Address,
    string Token,
    DateTime ExpiresOn
    );
