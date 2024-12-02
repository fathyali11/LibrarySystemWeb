namespace LibrarySystem.Domain.DTO.ApplicationUsers;
public record AccountUserRequest(
    string FirstName,
    string LastName,
    string Address
    );