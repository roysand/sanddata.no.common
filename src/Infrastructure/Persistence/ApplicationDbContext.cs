using System.Reflection;
using Application.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;


public partial class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IConfig _config;
    private readonly IDateTime _dateTimeService;

    public ApplicationDbContext(IConfig config, IDateTime dateTimeService)
    {
        _config = config;
        _dateTimeService = dateTimeService;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var sqlTimeout = 600;
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _config.ApplicationSettingsConfig.DbConnectionString();
            optionsBuilder.UseSqlServer(connectionString,
                    opts => opts.CommandTimeout(sqlTimeout))
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors(true)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }
}