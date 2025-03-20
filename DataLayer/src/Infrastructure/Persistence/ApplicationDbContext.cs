using System.Reflection;
using DataLayer.Application.Interface;
using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataLayer.Infrastructure.Persistence;

public partial class ApplicationDbContext(
    IConfig config,
    ILoggerFactory loggerFactory)
    : DbContext, IApplicationDbContext
{
    public DbSet<RawData> RawSet { get; set; } = null!;
    public DbSet<Detail> DetailSet { get; set; } = null!;
    public DbSet<Minute> MinuteSet { get; set; } = null!;
    public DbSet<Hour> HourSet { get; set; } = null!;
    public DbSet<Price> PriceSet { get; set; } = null!;
    public DbSet<PriceDetail> PriceDetailSet { get; set; } = null!;
    public DbSet<ExchangeRate> ExchangeRateSet { get; set; } = null!;
    public DbSet<Location> LocationSet { get; set; } = null!;
    public DbSet<UserLocation> UserLocationSet { get; set; } = null!;
    
    // Authorization
    public DbSet<User> UserSet { get; set; }
    public DbSet<ApiKey> ApiKeySet { get; set; }
    
    public async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var sqlTimeout = 600;
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString =
                config.ApplicationSettingsConfig.DbConnectionString();

            if (string.IsNullOrEmpty(connectionString) || connectionString.Trim().Length < 40)
            {
                throw new ApplicationException("No connection string configured.");
            }
            
            optionsBuilder.UseSqlServer(connectionString,
                    opts => opts.CommandTimeout(sqlTimeout))
                .EnableSensitiveDataLogging(config.ApplicationSettingsConfig.EnableSensitiveDataLogging())
                .EnableDetailedErrors(false)
                .UseLoggerFactory(loggerFactory);
        }

        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        OnModelCreatingPartial(modelBuilder);
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}