using DataLayer.Application.Interface;
using DataLayer.Application.Interface.Repositories;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Infrastructure.Repositories;

public class ApiKeyRepository(ApplicationDbContext appDbContext)
    : BaseRepository<ApiKey>(appDbContext), IApiKeyRepository<ApiKey>
{

    public Task<ApiKey?> GetWithChildren(string apiKey, CancellationToken cancellationToken)
    {
        var entity = _context.ApiKeySet.Where(w => w.Key == apiKey)
            .Include(a => a.Account)
            .FirstOrDefaultAsync(cancellationToken);

        return entity;
    }
}