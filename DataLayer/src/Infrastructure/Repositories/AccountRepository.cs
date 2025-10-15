using DataLayer.Application.Interface;
using DataLayer.Application.Interface.Repositories;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Persistence;

namespace DataLayer.Infrastructure.Repositories;

public class AccountRepository  : BaseRepository<Account>, IAccountRepository<Account>
{
    public AccountRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public Task<Account?> GetWithApiKey(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}