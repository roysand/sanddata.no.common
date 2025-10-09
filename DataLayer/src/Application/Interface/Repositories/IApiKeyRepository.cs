using DataLayer.Domain.Entities;

namespace DataLayer.Application.Interface.Repositories;

public interface IApiKeyRepository<T> : IBaseRepository<T> where T: class
{
    Task<ApiKey?> GetWithChildren(string apiKey, CancellationToken cancellationToken);
}