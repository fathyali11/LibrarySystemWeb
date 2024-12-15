namespace LibrarySystem.Services.Services.Tokens
{
    public interface ITokenServices
    {
        Task<OneOf<AuthResponse, Error>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
        (string token, DateTime expiresOn) GenerateToken(ApplicationUser user);
        (string refreshToken, DateTime expiresOn) GenerateRefreshToken();
        


    }
}
