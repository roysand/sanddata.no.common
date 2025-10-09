using DataLayer.Application.Interface.Repositories;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Persistence;

namespace DataLayer.Infrastructure.Repositories;

public class PriceRepository : BaseRepository<Price>, IPriceRepository<Price>
{
    public PriceRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }
}