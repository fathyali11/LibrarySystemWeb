using System.Linq.Expressions;
using LibrarySystem.Domain.DTO.Fines;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.IRepository;
public interface IFineRepository:IGenericRepository<Fine>
{
    Task<IEnumerable<FineResponse>> GetAllWithUserAndBookAsync(Expression<Func<Fine, bool>> predicate);
}
