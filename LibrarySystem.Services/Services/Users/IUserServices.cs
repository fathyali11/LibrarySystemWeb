using LibrarySystem.Domain.Abstractions.Pagination;
using LibrarySystem.Domain.DTO.Common;

namespace LibrarySystem.Services.Services.Users;
public interface IUserServices
{
    Task<OneOf<UserResponse, Error>> CreateUserAsync(CreateOrUpdateUserRequest request,CancellationToken cancellationToken=default);
    Task<OneOf<UserResponse, Error>> UpdateUserAsync(string userId,CreateOrUpdateUserRequest request,CancellationToken cancellationToken=default);
    Task<OneOf<UserResponse, Error>> GetUserByAsync(string userId, CancellationToken cancellationToken = default);
    Task<OneOf<PaginatedResult<UserResponse, UserResponse>, Error>> GetAllUsersAsync(PaginatedRequest request,CancellationToken cancellationToken = default);
    Task<OneOf<bool, Error>> ChangeUserActivationAsync(string userId, CancellationToken cancellationToken = default);
    Task<OneOf<bool, Error>> ChangeRoleOfUserAsync(string userId, ChangeUserRoleRequest request, CancellationToken cancellationToken = default);

}
