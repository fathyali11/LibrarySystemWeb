namespace LibrarySystem.Domain.DTO.ApplicationUsers;
public record RegistersRequest(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Address,
    string Password
    );