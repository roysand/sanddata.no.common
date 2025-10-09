using DataLayer.Domain.Entities;

namespace DataLayer.Application.Interface.Repositories;

public interface IAppUserRepository<T> : IBaseRepository<T> where T : class
{
    Task<AppUser?> GetWithChildren(string email, CancellationToken cancellationToken);
}
