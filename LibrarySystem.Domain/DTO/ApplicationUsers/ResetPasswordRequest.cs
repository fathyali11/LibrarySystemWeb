namespace LibrarySystem.Domain.DTO.ApplicationUsers;
public record ResetPasswordRequest(string UserId,string Token,string Password);