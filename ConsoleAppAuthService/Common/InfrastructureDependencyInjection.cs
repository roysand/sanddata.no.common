using ConsoleAppAuthService.Common.Interface;
using ConsoleAppAuthService.Common.Persistence;
using DataLayer.Application.Interface;
using DataLayer.Application.Interface.Repositories;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Persistence;
using DataLayer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleAppAuthService.Common;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfig config)
    {
        services.AddSingleton<IConfig>(_ => config);
        services.AddSingleton<ILoggerFactory, LoggerFactory>();
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.ApplicationSettingsConfig.DbConnectionString()));
        services.AddScoped<ApplicationDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<IPriceRepository<Price>, PriceRepository>();
        services.AddScoped<IAppUserRepository<AppUser>, AppUserRepository>();

        return services;
    }
}