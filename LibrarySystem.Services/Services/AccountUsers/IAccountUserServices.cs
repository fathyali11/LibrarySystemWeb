namespace LibrarySystem.Services.Services.AccountUsers
{
    public interface IAccountUserServices
    {
        Task<OneOf<AccountUserResponse, Error>> GetAsync(string userId, CancellationToken cancellationToken = default);
        Task<OneOf<bool, Error>> UpdateAsync(string userId,AccountUserRequest request,CancellationToken cancellationToken=default);
        Task<OneOf<bool, Error>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default);






    }
}
