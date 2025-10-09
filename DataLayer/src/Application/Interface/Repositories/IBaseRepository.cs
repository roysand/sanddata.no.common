using System.Linq.Expressions;

namespace DataLayer.Application.Interface.Repositories;

public interface IBaseRepository
{
}

public interface IBaseRepository<T> : IBaseRepository where T : class
{
    T Insert(T entity);
    T Update(T entity);
    bool Delete(T entity);
    Task<T?> Get(int id, CancellationToken cancellationToken);
    Task<T?> Get(string id, CancellationToken cancellationToken);
    Task<T?> Get(Guid id, CancellationToken cancellationToken);
    Task<T?> Get(object id, CancellationToken cancellationToken);

    Task<IEnumerable<T?>> Find(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken,
        bool noTrack = false);

    Task<IEnumerable<T?>> All(CancellationToken cancellationToken);
    Task<bool> Exists(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

    Task<int> SaveChanges(CancellationToken cancellationToken);
}