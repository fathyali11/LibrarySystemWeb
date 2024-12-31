namespace LibrarySystem.Domain.IRepository;
public interface IRoleRepository
{
    Task<IEnumerable<string>> GetPermissions(IEnumerable<string> roles, CancellationToken cancellationToken=default);
}
