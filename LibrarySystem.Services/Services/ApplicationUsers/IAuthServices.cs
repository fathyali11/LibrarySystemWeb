using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.DTO.ApplicationUsers;
using Microsoft.AspNetCore.Identity.Data;
using OneOf;
namespace LibrarySystem.Services.Services.ApplicationUsers
{
    public interface IAuthServices
    {
        Task<OneOf<AuthResponse, Error>> RegisterAsync(RegistersRequest request,CancellationToken cancellationToken=default);
        Task<OneOf<AuthResponse, Error>> LoginAsync(LoginsRequest request,CancellationToken cancellationToken=default);
        Task<OneOf<AuthResponse,Error>> RefreshTokenAsync(RefreshTokenRequest request,CancellationToken cancellationToken=default);
    }
}
