using System.Reflection;
using DataLayer.Application.Interface;
using DataLayer.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataLayer.Infrastructure.Persistence;

public partial class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
{
    private readonly IConfig _config;
    private readonly ILoggerFactory _loggerFactory;

    public ApplicationDbContext(IConfig config, ILoggerFactory loggerFactory)
        : base(GetOptions(config))
    {
        _config = config;
        _loggerFactory = loggerFactory;
    }
    
    private static DbContextOptions<ApplicationDbContext> GetOptions(IConfig config)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(config.ApplicationSettingsConfig.DbConnectionString());
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
    
    // Authorization
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
                _config.ApplicationSettingsConfig.DbConnectionString();

            if (string.IsNullOrEmpty(connectionString) || connectionString.Trim().Length < 40)
            {
                throw new ApplicationException("No connection string configured.");
            }
            
            optionsBuilder.UseSqlServer(connectionString,
                    opts =>
                    {
                        opts.CommandTimeout(sqlTimeout);
                        opts.EnableRetryOnFailure();
                    })
                .EnableSensitiveDataLogging(_config.ApplicationSettingsConfig.EnableSensitiveDataLogging())
                .EnableDetailedErrors(false)
                .UseLoggerFactory(_loggerFactory);
        }

        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        OnModelCreatingPartial(modelBuilder);
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}