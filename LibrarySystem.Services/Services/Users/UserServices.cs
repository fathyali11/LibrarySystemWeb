namespace LibrarySystem.Services.Services.Users;
public class UserServices(IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IMapper mapper):IUserServices
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly IMapper _mapper = mapper;
    public async Task<OneOf<UserResponse, Error>> CreateUserAsync(CreateOrUpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var roleIsExist = await _roleManager.Roles.AnyAsync(x => string.Equals(x.Name, request.Role));
        if(!roleIsExist)
            return RoleErrors.NotExist;

        var user = _mapper.Map<ApplicationUser>(request);
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return new Error(result.Errors.First().Code, result.Errors.First().Description, StatusCodes.Status400BadRequest);

        var roleResult = await _userManager.AddToRoleAsync(user, request.Role);
        if (!roleResult.Succeeded)
            return new Error(roleResult.Errors.First().Code, roleResult.Errors.First().Description, StatusCodes.Status400BadRequest);

        user.EmailConfirmed= true;
        await _unitOfWork.SaveChanges(cancellationToken);


        var response = _mapper.Map<UserResponse>(user);
        response.Role = request.Role;
        return response;
    }
    public async Task<OneOf<UserResponse, Error>> UpdateUserAsync(string userId,CreateOrUpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return UserErrors.NotFound;

        var roleIsExist = await _roleManager.Roles.AnyAsync(x => string.Equals(x.Name, request.Role));
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

        if(!string.Equals(userOldRole.First(),request.Role))
        {
            var roleResult = await _userManager.RemoveFromRoleAsync(user, userOldRole.First());
            if (!roleResult.Succeeded)
                return new Error(roleResult.Errors.First().Code, roleResult.Errors.First().Description, StatusCodes.Status400BadRequest);
            var roleResultNew = await _userManager.AddToRoleAsync(user, request.Role);
            if (!roleResultNew.Succeeded)
                return new Error(roleResultNew.Errors.First().Code, roleResultNew.Errors.First().Description, StatusCodes.Status400BadRequest);
        }
        await _unitOfWork.SaveChanges(cancellationToken);
        var response = _mapper.Map<UserResponse>(user);
        response.Role = request.Role;
        return response;
    }
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
    public async Task<OneOf<List<UserResponse>, Error>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        var users=await _unitOfWork.UserRepository.GetAll(cancellationToken);
        return users;
    }
    public async Task<OneOf<bool, Error>> ChangeUserActivationAsync(string userId, CancellationToken cancellationToken = default)
    {
        var userStatus = _userManager.Users.Where(x => x.Id == userId).Select(x => new {isActive=x.IsActive});
        var result= await userStatus.FirstOrDefaultAsync(cancellationToken);
        if (result is null)
            return UserErrors.NotFound;

        await _userManager.Users
            .Where(x=>x.Id==userId)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsActive, !result.isActive));
        await _unitOfWork.SaveChanges(cancellationToken);
        return true;
    }
}


