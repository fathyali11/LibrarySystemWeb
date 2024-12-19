using Hangfire;

namespace LibrarySystem.Services.Services.AuthUsers
{
    public class AuthServices(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILogger<AuthServices> logger,
        IEmailSender emailSender,
        IHttpContextAccessor httpContextAccessor,
        ITokenServices tokenServices) :IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<AuthServices> _logger = logger;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ITokenServices _tokenServices = tokenServices;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        
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

            await SendConfirmEmail(user);
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

            await SendConfirmEmail(user);
            return true;

        }

        public async Task<OneOf<bool, Error>> ForgetPasswordAsync(ForgetPasswordRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)
                return true;

            if(!user.EmailConfirmed)
                return UserErrors.EmailNotConfirmed;

            await SendResetPassword(user);

            return true;
        }

        public async Task<OneOf<bool, Error>> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
                return true;
            IdentityResult result;
            try
            {
                var token=Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Password));
                result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            }
            catch(FormatException)
            {
                return UserErrors.InValidToken;
            }
            if(!result.Succeeded)
            {
                var error = result.Errors.First();
                return new Error(error.Code, error.Description, StatusCodes.Status400BadRequest);
            }
            return true;
        }
        private async Task<AuthResponse> GenerateResponse(ApplicationUser user)
        {
            var response = _mapper.Map<AuthResponse>(user);

            var tokenCreation = _tokenServices.GenerateToken(user);
            response.Token = tokenCreation.token;
            response.ExpiresOn = tokenCreation.expiresOn;


            var refreshTokenCreation = _tokenServices.GenerateRefreshToken();
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

        private async Task SendConfirmEmail(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var origin = _httpContextAccessor.HttpContext!.Response.Headers.Origin;
            var confirmationLink = $"{origin}api/confirm-email?token={token}&userId={user.Id}";

            var keyValues = new Dictionary<string, string>()
            {
                {"{ConfirmationLink}",confirmationLink}
            };
            var emailBody = EmailHelper.PrepareBodyTemplate(PathsValues.TemplatesPaths, "EmailTemplate.html", keyValues);


            _logger.LogInformation($"\ntoken:{token}\nid={user.Id}\n");
            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "Confirm Your Email", emailBody));
            //await _emailSender.SendEmailAsync(user.Email!, "Confirm Your Email", emailBody);
            _logger.LogInformation("email was sent");
        }

        private async Task SendResetPassword(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var origin = _httpContextAccessor.HttpContext!.Response.Headers.Origin;
            var resetLink = $"{origin}api/reset-password?token={token}&userId={user.Id}";

            var keyValues = new Dictionary<string, string>()
            {
                {"{resetLink}",resetLink}
            };
            var emailBody = EmailHelper.PrepareBodyTemplate(PathsValues.TemplatesPaths, "ResetPasswordTemplate.html", keyValues);


            _logger.LogInformation($"\ntoken:{token}\nid={user.Id}\n");
            BackgroundJob.Enqueue(()=> _emailSender.SendEmailAsync(user.Email!, "Reset Your Password", emailBody));
            //await _emailSender.SendEmailAsync(user.Email!, "Reset Your Password", emailBody);
            _logger.LogInformation("email was sent");
        }
    }
}
