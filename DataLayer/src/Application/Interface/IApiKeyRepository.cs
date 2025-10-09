using DataLayer.Domain.Entities;
using sanddata.no.ams.api.Application.Common.Interfaces.Repositories;

namespace DataLayer.Application.Interface;

public interface IApiKeyRepository<T> : IEFRepository<T> where T: class
{
    Task<ApiKey?> GetWithChildren(string apiKey, CancellationToken cancellationToken);
}