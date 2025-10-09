using DataLayer.Application.Interface;
using DataLayer.Application.Interface.Repositories;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Persistence;

namespace DataLayer.Infrastructure.Repositories;

public class PowerUsageRepository : BaseRepository<Hour>, IPowerUsageRepository<Hour>
{
    public PowerUsageRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }
}