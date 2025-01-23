using LibrarySystem.Domain.Abstractions.Pagination;
using LibrarySystem.Domain.DTO.Common;
using System.Linq.Dynamic.Core;
namespace LibrarySystem.Services.Services.Users;
/// <include file='ExternalServicesDocs\UserServicesDocs.xml' path='/docs/members[@name="userServices"]/UserServices'/>
public class UserServices(IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IMapper mapper,HybridCache hybridCache) : IUserServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly IMapper _mapper = mapper;
    private readonly HybridCache _hybridCache = hybridCache;

    /// <include file='ExternalServicesDocs\UserServicesDocs.xml' path='/docs/members[@name="userServices"]/CreateUserAsync'/>
    public async Task<OneOf<UserResponse, Error>> CreateUserAsync(CreateOrUpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var roleIsExist = await _roleManager.Roles.AnyAsync(x => x.Name!.ToLower() == request.Role.ToLower());
        if (!roleIsExist)
            return RoleErrors.NotExist;

        var user = _mapper.Map<ApplicationUser>(request);
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return new Error(result.Errors.First().Code, result.Errors.First().Description, StatusCodes.Status400BadRequest);

        var roleResult = await _userManager.AddToRoleAsync(user, request.Role);
        if (!roleResult.Succeeded)
            return new Error(roleResult.Errors.First().Code, roleResult.Errors.First().Description, StatusCodes.Status400BadRequest);

        user.EmailConfirmed = true;
        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync("AllUsers",cancellationToken);

        var response = _mapper.Map<UserResponse>(user);
        response.Role = request.Role;
        return response;
    }
    /// <include file='ExternalServicesDocs\UserServicesDocs.xml' path='/docs/members[@name="userServices"]/UpdateUserAsync'/>
    public async Task<OneOf<UserResponse, Error>> UpdateUserAsync(string userId, CreateOrUpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return UserErrors.NotFound;

        var roleIsExist = await _roleManager.Roles.AnyAsync(x => x.Name!.ToLower() == request.Role.ToLower());
        if (!roleIsExist)
            return RoleErrors.NotExist;

        await _userManager.Users
            .Where(x => x.Id == userId)
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.FirstName, request.FirstName)
                .SetProperty(p => p.LastName, request.LastName)
                .SetProperty(p => p.Email, request.Email)
                .SetProperty(p => p.UserName, request.UserName)
                .SetProperty(p => p.Address, request.Address)
                .SetProperty(p => p.PhoneNumber, request.PhoneNumber)
        );

        var userOldRole = await _userManager.GetRolesAsync(user);

        if (!string.Equals(userOldRole.First(), request.Role))
        {
            var roleResult = await _userManager.RemoveFromRoleAsync(user, userOldRole.First());
            if (!roleResult.Succeeded)
                return new Error(roleResult.Errors.First().Code, roleResult.Errors.First().Description, StatusCodes.Status400BadRequest);
            var roleResultNew = await _userManager.AddToRoleAsync(user, request.Role);
            if (!roleResultNew.Succeeded)
                return new Error(roleResultNew.Errors.First().Code, roleResultNew.Errors.First().Description, StatusCodes.Status400BadRequest);
        }
        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync("AllUsers", cancellationToken);
        var response = _mapper.Map<UserResponse>(user);
        response.Role = request.Role;
        return response;
    }
    /// <include file='ExternalServicesDocs\UserServicesDocs.xml' path='/docs/members[@name="userServices"]/GetUserByAsync'/>
    public async Task<OneOf<UserResponse, Error>> GetUserByAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return UserErrors.NotFound;
        var response = _mapper.Map<UserResponse>(user);
        var role = await _userManager.GetRolesAsync(user);
        response.Role = role.First();
        return response;
    }
    /// <include file='ExternalServicesDocs\UserServicesDocs.xml' path='/docs/members[@name="userServices"]/GetAllUsersAsync'/>
    public async Task<OneOf<PaginatedResult<UserResponse,UserResponse>, Error>> GetAllUsersAsync(PaginatedRequest request,CancellationToken cancellationToken = default)
    {
        const string cacheKey= "AllUsers";
        var users = await _hybridCache.GetOrCreateAsync(cacheKey,
            async _ =>
            {
                var query = _unitOfWork.UserRepository.GetAll(cancellationToken);
                return await query.ToListAsync(cancellationToken);
            });

        if(!string.IsNullOrEmpty(request.SearchTerm))
            users=users.Where(x=>
            x.FirstName.Contains(request.SearchTerm,StringComparison.OrdinalIgnoreCase) ||
            x.LastName.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
            x.Email.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) || 
            x.UserName.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

        if(!string.IsNullOrEmpty(request.SortTerm))
            users=users.AsQueryable()
                .OrderBy($"{request.SortTerm} {request.SortBy}").ToList();

        var paginatedUsers = PaginatedResult<UserResponse,UserResponse>.Create(users, request.PageNumber, request.PageSize);
        paginatedUsers.Result = _mapper.Map<List<UserResponse>>(paginatedUsers.Values);
        return paginatedUsers;

    }
    /// <include file='ExternalServicesDocs\UserServicesDocs.xml' path='/docs/members[@name="userServices"]/ChangeUserActivationAsync'/>
    public async Task<OneOf<bool, Error>> ChangeUserActivationAsync(string userId, CancellationToken cancellationToken = default)
    {
        var userStatus = _userManager.Users.Where(x => x.Id == userId).Select(x => new { isActive = x.IsActive });
        var result = await userStatus.FirstOrDefaultAsync(cancellationToken);
        if (result is null)
            return UserErrors.NotFound;

        await _userManager.Users
            .Where(x => x.Id == userId)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsActive, !result.isActive));
        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync("AllUsers", cancellationToken);
        return true;
    }
    /// <include file='ExternalServicesDocs\UserServicesDocs.xml' path='/docs/members[@name="userServices"]/ChangeRoleOfUserAsync'/>
    public async Task<OneOf<bool, Error>> ChangeRoleOfUserAsync(string userId, ChangeUserRoleRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return UserErrors.NotFound;

        var roleIsExist = await _roleManager.Roles.AnyAsync(x => x.Name!.ToLower() == request.Role.ToLower());
        if (!roleIsExist)
            return RoleErrors.NotExist;

        var userOldRole = await _userManager.GetRolesAsync(user);
        if (string.Equals(request.Role, userOldRole.First(), StringComparison.OrdinalIgnoreCase))
            return true;

        var roleRemoveResult = await _userManager.RemoveFromRoleAsync(user, userOldRole.First());
        if (!roleRemoveResult.Succeeded)
            return new Error(roleRemoveResult.Errors.First().Code, roleRemoveResult.Errors.First().Description, StatusCodes.Status400BadRequest);

        var roleCreateResult = await _userManager.AddToRoleAsync(user, request.Role);
        if (!roleCreateResult.Succeeded)
            return new Error(roleCreateResult.Errors.First().Code, roleCreateResult.Errors.First().Description, StatusCodes.Status400BadRequest);

        await _unitOfWork.SaveChanges(cancellationToken);
        await _hybridCache.RemoveAsync("AllUsers", cancellationToken);
        return true;

    }
}