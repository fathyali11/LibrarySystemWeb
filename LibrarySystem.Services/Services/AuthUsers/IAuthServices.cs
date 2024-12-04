using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.DTO.ApplicationUsers;
using OneOf;
namespace LibrarySystem.Services.Services.AuthUsers
{
    public interface IAuthServices
    {
        Task<OneOf<bool, Error>> RegisterAsync(RegistersRequest request,CancellationToken cancellationToken=default);
        Task<OneOf<AuthResponse, Error>> LoginAsync(LoginsRequest request,CancellationToken cancellationToken=default);
        Task<OneOf<bool, Error>> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken = default);
        Task<OneOf<bool, Error>> ResendConfirmEmailAsync(ResendConfirmEmailRequest request, CancellationToken cancellationToken = default);
        Task<OneOf<bool, Error>> ForgetPasswordAsync(ForgetPasswordRequest request, CancellationToken cancellationToken=default);
        Task<OneOf<bool, Error>> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken cancellationToken=default);





    }
}
