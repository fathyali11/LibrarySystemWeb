namespace LibrarySystem.Domain.DTO.ApplicationUsers;
public record AccountUserResponse(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Address
    );