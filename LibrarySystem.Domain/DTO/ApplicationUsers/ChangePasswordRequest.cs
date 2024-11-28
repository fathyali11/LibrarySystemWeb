namespace LibrarySystem.Domain.DTO.ApplicationUsers
{
    public record ChangePasswordRequest(
        string Email,
        string OldPassword,
        string NewPassword
        );
}
