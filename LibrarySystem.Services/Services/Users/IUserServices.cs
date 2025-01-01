namespace LibrarySystem.Services.Services.Users;
public interface IUserServices
{
    Task<OneOf<UserResponse, Error>> CreateUserAsync(CreateOrUpdateUserRequest request,CancellationToken cancellationToken=default);
    Task<OneOf<UserResponse, Error>> UpdateUserAsync(string userId,CreateOrUpdateUserRequest request,CancellationToken cancellationToken=default);
    Task<OneOf<UserResponse, Error>> GetUserByAsync(string userId, CancellationToken cancellationToken = default);
    Task<OneOf<List<UserResponse>, Error>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<OneOf<bool, Error>> ChangeUserActivationAsync(string userId, CancellationToken cancellationToken = default);
}
