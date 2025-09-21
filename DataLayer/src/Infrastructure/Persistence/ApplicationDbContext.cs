using System.Reflection;
using DataLayer.Application.Interface;
using DataLayer.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataLayer.Infrastructure.Persistence;

public partial class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IConfig _config;
    private readonly ILoggerFactory _loggerFactory;

    public ApplicationDbContext(IConfig config, ILoggerFactory loggerFactory
        , DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        _config = config;
        _loggerFactory = loggerFactory;
    }
    
    private static DbContextOptions<ApplicationDbContext> GetOptions()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        return optionsBuilder.Options;
    }
    
    public DbSet<RawData> RawSet { get; set; } = null!;
    public DbSet<Detail> DetailSet { get; set; } = null!;
    public DbSet<Minute> MinuteSet { get; set; } = null!;
    public DbSet<Hour> HourSet { get; set; } = null!;
    public DbSet<Price> PriceSet { get; set; } = null!;
    public DbSet<PriceDetail> PriceDetailSet { get; set; } = null!;
    public DbSet<ExchangeRate> ExchangeRateSet { get; set; } = null!;
    public DbSet<Location> LocationSet { get; set; } = null!;
    public DbSet<AppUserLocation> AppUserLocationSet { get; set; }

    // Authorization
    public DbSet<ApiKey> ApiKeySet { get; set; }
    public DbSet<AppUser> AppUserSet { get; set; }
    
    public async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        OnModelCreatingPartial(modelBuilder);
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}