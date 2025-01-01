using LibrarySystem.Domain.DTO.ApplicationUsers;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.IRepository
{
    public interface IUserRepository:IGenericRepository<ApplicationUser>
    {
        Task<bool> IsExistAsync(string userName, string email);
        Task<List<UserResponse>> GetAll(CancellationToken cancellationToken = default);
    }
}
