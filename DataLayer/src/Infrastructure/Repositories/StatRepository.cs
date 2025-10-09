using DataLayer.Application.Interface;
using DataLayer.Application.Interface.Repositories;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Infrastructure.Repositories
{
    public class StatRepository : BaseRepository<Detail> , IStatRepository<Detail>
    {
        public StatRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}