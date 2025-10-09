using DataLayer.Application.Interface;
using DataLayer.Infrastructure.Persistence;

namespace DataLayer.Infrastructure.Repositories;

public class LocationRepository : BaseRepository<DataLayer.Domain.Entities.Location>, ILocationRepository<DataLayer.Domain.Entities.Location>
{
    public LocationRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }
}