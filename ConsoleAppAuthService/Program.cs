// See https://aka.ms/new-console-template for more information

using DataLayer.Application.Interface.Repositories;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Config;
using System.IO;
using ConsoleAppAuthService.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


Console.WriteLine("Hello, World!");

// Build configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
    .Build();

var config = new Config(configuration);

// Register services
var services = new ServiceCollection();
services.AddInfrastructureServices(config);
var serviceProvider = services.BuildServiceProvider();

// Resolve and use the repository
using (var scope = serviceProvider.CreateScope())
{
    var priceRepository = scope.ServiceProvider.GetRequiredService<IPriceRepository<Price>>();
    var appUserRepository = scope.ServiceProvider.GetRequiredService<IAppUserRepository<AppUser>>();
    
    Console.WriteLine("Repository resolved successfully.");
    var data = await priceRepository.Find(w => w.PricePeriod >= DateTime.Now.AddDays(-1), CancellationToken.None,
        true);

    var users = await appUserRepository.All(CancellationToken.None, true);
    Console.WriteLine($"Repository used successfully. {data.Count()} {users.Count()}");
}

// Use repo here