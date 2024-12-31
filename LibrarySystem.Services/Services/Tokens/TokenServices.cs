namespace LibrarySystem.Services.Services.Tokens
{
    public class TokenServices(IOptions<JwtOptions> options,
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILogger<TokenServices> logger) :ITokenServices
    {
        private readonly JwtOptions _jwtOptions = options.Value;
        private readonly IUnitOfWork _unitOfWork= unitOfWork;
        private readonly ILogger<TokenServices> _logger = logger;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        private readonly int _refreshTokenExpirationOnDays = 7;
        public async Task<OneOf<AuthResponse, Error>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            var userId = ValidateToken(request.token);
            if (userId is null)
                return UserErrors.InValidToken;

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return UserErrors.InValidToken;

            var refreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == request.refreshToken && x.IsActive);
            if (refreshToken is null)
                return UserErrors.InValidRefreshToken;

            refreshToken.RevokedOn = DateTime.UtcNow;
            return await GenerateResponse(user);

        }

        public (string token, DateTime expiresOn) GenerateToken(ApplicationUser user,IEnumerable<string>roles,IEnumerable<string>permissions)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                 new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                 new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                 new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                 new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(nameof(roles),JsonSerializer.Serialize(roles),JsonClaimValueTypes.JsonArray),
                 new Claim(nameof(permissions),JsonSerializer.Serialize(permissions),JsonClaimValueTypes.JsonArray),
            };


            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: expiration
                );


            return (new JwtSecurityTokenHandler().WriteToken(token), expiration);
        }

        public (string refreshToken, DateTime expiresOn) GenerateRefreshToken()
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var expiresOn = DateTime.UtcNow.AddDays(_refreshTokenExpirationOnDays);
            return (token!, expiresOn);
        }

        private string? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));


            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = securityKey,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;

                return jwtToken!.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)!.Value;
            }
            catch
            {
                _logger.LogInformation("token is not valid as validation returns null");
                return null;
            }
        }

        private async Task<AuthResponse> GenerateResponse(ApplicationUser user)
        {
            var response = _mapper.Map<AuthResponse>(user);

            var roles = await _userManager.GetRolesAsync(user);
            var permissions = await _unitOfWork.RoleRepository.GetPermissions(roles);

            var tokenCreation = GenerateToken(user, roles, permissions);
            response.Token = tokenCreation.token;
            response.ExpiresOn = tokenCreation.expiresOn;


            var refreshTokenCreation = GenerateRefreshToken();
            response.RefreshToken = refreshTokenCreation.refreshToken;
            response.RefreshTokenExpiration = refreshTokenCreation.expiresOn;

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshTokenCreation.refreshToken,
                ExpiresOn = refreshTokenCreation.expiresOn
            });
            await _unitOfWork.SaveChanges();
            return response;
        }

    }
}
