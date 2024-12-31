namespace LibrarySystem.Services.Services.Tokens
{
    public interface ITokenServices
    {
        Task<OneOf<AuthResponse, Error>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
        (string token, DateTime expiresOn) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions);
        (string refreshToken, DateTime expiresOn) GenerateRefreshToken();
        


    }
}
