using DataLayer.Domain.Entities;
using sanddata.no.ams.api.Application.Common.Interfaces.Repositories;

namespace DataLayer.Application.Interface;

public interface IAppUserRepository<T> : IEFRepository<T> where T : class
{
    Task<AppUser?> GetWithChildren(string email, CancellationToken cancellationToken);
}
