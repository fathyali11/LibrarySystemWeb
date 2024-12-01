using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using LibrarySystem.Data.Repository;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.ConstValues;
using LibrarySystem.Domain.Abstractions.Errors;
using LibrarySystem.Domain.DTO.ApplicationUsers;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Services.Services.Emails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using OneOf;

namespace LibrarySystem.Services.Services.AuthUsers
{
    public class AuthServices(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper,
        IOptions<JwtOptions> options,
        IUnitOfWork unitOfWork,
        ILogger<AuthServices> logger,
        IEmailSender emailSender,
        IHttpContextAccessor httpContextAccessor) :IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly JwtOptions _jwtOptions = options.Value;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<AuthServices> _logger = logger;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        private readonly int _refreshTokenExpirationOnDays=7;
        public async Task<OneOf<bool, Error>> RegisterAsync(RegistersRequest request, CancellationToken cancellationToken = default)
        {
            var userIsExists = await _unitOfWork.UserRepository.IsExistAsync(request.UserName, request.Email);
            if (userIsExists)
                return UserErrors.IsFound;
            var isGmailEmail = request.Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase);
            if (!isGmailEmail)
                return UserErrors.InValidEmailype;

            var user = _mapper.Map<ApplicationUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var error = result.Errors.FirstOrDefault();
                return new Error(error!.Code, error.Description, StatusCodes.Status400BadRequest);
            }

            await SendEmail(user);
            return true;
        }

        public async Task<OneOf<AuthResponse, Error>> LoginAsync(LoginsRequest request, CancellationToken cancellationToken = default)
        {
            var user=await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return UserErrors.InValid;

            var result=await _signInManager.PasswordSignInAsync(user, request.Password,false,false);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    return UserErrors.IsLocked;
                else if(result.IsNotAllowed)
                    return UserErrors.EmailNotConfirmed;
                else
                    return UserErrors.InValid;
            }
            var hasRefreshToken = user.RefreshTokens.Where(x => x.IsActive).ToList();
            if(hasRefreshToken.Any())
            {
                foreach (var refreshToken in hasRefreshToken)
                    refreshToken.RevokedOn=DateTime.UtcNow;
            }
            return await GenerateResponse(user);
        }

        public async Task<OneOf<AuthResponse, Error>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            var userId = ValidateToken(request.token);
            if (userId is null)
                return UserErrors.InValidToken;
            var user=await _userManager.FindByIdAsync(userId);
            if(user is null)
                return UserErrors.InValidToken;
            var refreshToken=user.RefreshTokens.SingleOrDefault(x=>x.Token==request.refreshToken&&x.IsActive);
            if(refreshToken is null)
                return UserErrors.InValidRefreshToken;
            refreshToken.RevokedOn=DateTime.UtcNow;

            return await GenerateResponse(user);

        }

        public async Task<OneOf<bool, Error>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if( user is null)
                return UserErrors.NotFound;
            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if(!result.Succeeded)
            {
                var error = result.Errors.First();
                return new Error(error.Code,error.Description,StatusCodes.Status400BadRequest);
            }
            return true;
        }

        public async Task<OneOf<bool,Error>> ConfirmEmailAsync(ConfirmEmailRequest request,CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if(user is null)
                return UserErrors.NotFound;

            if (user.EmailConfirmed)
                return UserErrors.EmailConfirmed;
            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ConfirmEmailAsync(user,token);
            if(!result.Succeeded)
            {
                _logger.LogInformation("email not confirmed");
                var error = result.Errors.First();
                return new Error(error.Code, error.Description, StatusCodes.Status400BadRequest);
            }
            _logger.LogInformation("email confirmed");
            await _unitOfWork.SaveChanges(cancellationToken);
            return true;

        }

        public async Task<OneOf<bool,Error>> ResendConfirmEmailAsync(ResendConfirmEmailRequest request,CancellationToken cancellationToken=default)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if( user is null) 
                return UserErrors.NotFound;

            await SendEmail(user);
            return true;

        }


        private (string token,DateTime expiresOn) GenerateTokenAsync(ApplicationUser user)
        {
            var securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                 new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                 new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                 new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                 new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims:claims,
                signingCredentials:signingCredentials,
                expires:expiration
                );
            

            return (new JwtSecurityTokenHandler().WriteToken(token), expiration);
        }
        private (string refreshToken,DateTime expiresOn) GenerateRefreshTokenAsync()
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

            var tokenCreation = GenerateTokenAsync(user);
            response.Token = tokenCreation.token;
            response.ExpiresOn = tokenCreation.expiresOn;


            var refreshTokenCreation = GenerateRefreshTokenAsync();
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

        private async Task SendEmail(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var origin = _httpContextAccessor.HttpContext!.Response.Headers.Origin;
            var confirmationLink = $"{origin}api/confirm-email?token={token}&userId={user.Id}";

            var keyValues = new Dictionary<string, string>()
            {
                {"ConfirmationLink",confirmationLink}
            };
            var emailBody = EmailHelper.PrepareBodyTemplate(PathsValues.TemplatesPaths, "EmailTemplate.html", keyValues);


            _logger.LogInformation($"\ntoken:{token}\nid={user.Id}\n");
            await _emailSender.SendEmailAsync(user.Email!, "Confirm Your Email", emailBody);
            _logger.LogInformation("email was sent");
        }
    }
}
