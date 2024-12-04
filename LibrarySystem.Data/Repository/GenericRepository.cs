using LibrarySystem.Data.Data;
using LibrarySystem.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibrarySystem.Data.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null,
        string? includedNavigations = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? searchTerm = null,
        Expression<Func<T, bool>>? searchExpression = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbSet.AsQueryable();
        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        
        if (includedNavigations is not null)
        {
            foreach (var includedNavigation in includedNavigations.Split(","))
            {
                query = query.Include(includedNavigation);
            }
        }

        
        if (!string.IsNullOrEmpty(searchTerm) && searchExpression is not null)
        {
            query = query.Where(searchExpression);
        }

        
        if (orderBy is not null)
        {
            query = orderBy(query);
        }

       
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByAsync(Expression<Func<T, bool>>? predicate, string? includedNavigations, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbSet.AsQueryable();
        if (predicate is not null)
        {
            query = query.Where(predicate);
        }
        if (includedNavigations is not null)
        {
            foreach (var includedNavigation in includedNavigations.Split(","))
                query = query.Include(includedNavigation);
        }
        return await query.FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    public async Task<bool> IsExits(Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken = default)
    {
        if(predicate is null)
            return false;

        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }
    public async Task<T?> ExitsOrNot(Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            return null;

        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }
    public async Task<T?> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity,cancellationToken);
        return entity;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }


}
