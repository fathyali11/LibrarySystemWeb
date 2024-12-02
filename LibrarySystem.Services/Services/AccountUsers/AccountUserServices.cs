using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.Abstractions.Errors;
using LibrarySystem.Domain.DTO.ApplicationUsers;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace LibrarySystem.Services.Services.AccountUsers
{
    public class AccountUserServices(UserManager<ApplicationUser> userManager,
        IMapper mapper):IAccountUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        public async Task<OneOf<AccountUserResponse, Error>> GetAsync(string userId, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.Users
                .Where(x => x.Id == userId)
                .ProjectTo<AccountUserResponse>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            return user!;
        }
        public async Task<OneOf<bool,Error>> UpdateAsync(string userId, AccountUserRequest request, CancellationToken cancellationToken = default)
        {
            var user=await _userManager.FindByIdAsync(userId);
            if (user == null)
                return UserErrors.NotFound;

            await _userManager.Users
                .Where(x => x.Id == user.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.FirstName, request.FirstName)
                .SetProperty(x => x.LastName, request.LastName)
                .SetProperty(x => x.Address, request.Address));

            return true;
        }

        public async Task<OneOf<bool, Error>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return UserErrors.NotFound;
            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                var error = result.Errors.First();
                return new Error(error.Code, error.Description, StatusCodes.Status400BadRequest);
            }
            return true;
        }
    }
}
