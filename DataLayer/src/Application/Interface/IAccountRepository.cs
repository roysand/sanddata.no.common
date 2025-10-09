using sanddata.no.ams.api.Application.Common.Interfaces.Repositories;

namespace DataLayer.Application.Interface;

public interface IAccountRepository<T> : IEFRepository<T> where T: class
{
    Task<T?> GetWithApiKey(Guid id, CancellationToken cancellationToken);
}