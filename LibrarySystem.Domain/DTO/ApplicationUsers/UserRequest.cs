namespace LibrarySystem.Domain.DTO.ApplicationUsers;
public record UserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Address,
    string Password
    );