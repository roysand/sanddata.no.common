using ConsoleAppAuthService.Common.Interface;
using DataLayer.Application.Interface;
using DataLayer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConsoleAppAuthService.Common.Persistence;

public class AppDbContext : ApplicationDbContext,IAppDbContext
{
    private readonly IConfig _config;
    private readonly ILoggerFactory _loggerFactory;

    public AppDbContext(IConfig config, ILoggerFactory loggerFactory)
         : base(config, loggerFactory, new DbContextOptions<AppDbContext>())
    {
        _config = config;
        _loggerFactory = loggerFactory;
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
                .EnableDetailedErrors(true)
                .UseLoggerFactory(_loggerFactory);
        }    
    }
}
