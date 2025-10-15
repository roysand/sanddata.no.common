namespace DataLayer.Application.Interface.Repositories;

public interface IAccountRepository<T> : IBaseRepository<T> where T: class
{
    Task<T?> GetWithApiKey(Guid id, CancellationToken cancellationToken);
}