using DOMAIN.Base;

namespace INFRA.Repositories.Common;

public interface IRepository<T> where T : AggregateRoot
{

    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void SaveChangesAsync();
}
