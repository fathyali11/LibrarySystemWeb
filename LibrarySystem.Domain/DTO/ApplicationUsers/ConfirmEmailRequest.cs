namespace LibrarySystem.Domain.DTO.ApplicationUsers;
public record ConfirmEmailRequest(string Token,string UserId);