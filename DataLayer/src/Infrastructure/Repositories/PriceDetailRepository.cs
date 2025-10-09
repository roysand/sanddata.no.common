using DataLayer.Application.Interface;
using DataLayer.Application.Interface.Repositories;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Persistence;

namespace DataLayer.Infrastructure.Repositories;

public class PriceDetailRepository : BaseRepository<PriceDetail>, IPriceDetailRepository<PriceDetail>
{
    public PriceDetailRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }
}