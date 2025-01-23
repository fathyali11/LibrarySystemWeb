using System.Linq.Expressions;

namespace LibrarySystem.Domain.IRepository;
public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll(
         Expression<Func<T, bool>>? predicate = null,
         string? includedNavigations = null,
         CancellationToken cancellationToken = default);
    Task<T?> GetByAsync(Expression<Func<T, bool>>? predicate, string? includedNavigations=null, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id);
    Task<bool> IsExits(Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken = default);
    Task<T?> ExitsOrNot(Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken = default);
    Task<T?> AddAsync(T entity, CancellationToken cancellationToken = default);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
}