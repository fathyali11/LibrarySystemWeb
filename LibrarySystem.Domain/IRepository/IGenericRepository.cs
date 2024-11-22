using System.Linq.Expressions;

namespace LibrarySystem.Domain.IRepository;
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(
         Expression<Func<T, bool>>? predicate = null,
         string? includedNavigations = null,
         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
         string? searchTerm = null,
         Expression<Func<T, bool>>? searchExpression = null,
         CancellationToken cancellationToken = default);
    Task<T?> GetByAsync(Expression<Func<T, bool>>? predicate, string? includedNavigations, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id);
    Task<T?> AddAsync(T entity, CancellationToken cancellationToken = default);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
}