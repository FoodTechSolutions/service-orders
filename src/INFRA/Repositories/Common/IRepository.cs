using DOMAIN.Base;
using System.Linq.Expressions;

namespace INFRA.Repositories.Common;

public interface IRepository<T> where T : AggregateRoot
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
    Task AddAsync(T entity);
    void SaveChangesAsync();
}
