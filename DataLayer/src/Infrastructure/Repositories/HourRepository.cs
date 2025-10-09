using AutoMapper;
using DataLayer.Application.Interface;
using DataLayer.Application.Interface.Repositories;
using DataLayer.Application.Models.CommandsAndQueries.Location.Common;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Infrastructure.Repositories;

public class HourRepository : BaseRepository<Hour>, IHourRepository<Hour>
{
    public HourRepository(ApplicationDbContext context) : base(context)
    {
    }
}