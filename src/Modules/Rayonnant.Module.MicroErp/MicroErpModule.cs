using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor;
using Rayonnant.Core.Modularity;
using Rayonnant.Module.MicroErp.Data;
using Rayonnant.Module.MicroErp.Services;

namespace Rayonnant.Module.MicroErp;

public class MicroErpModule : IModule
{
    public ModuleInfo Info => new(
        Name: "MicroERP", 
        Description: "System zarządzania zamówieniami PCB", 
        Icon: Icons.Material.Filled.PrecisionManufacturing, 
        RoutePrefix: "/microerp", 
        Order: 3
    );

    public void ConfigureServices(IServiceCollection services)
    {
        // Register the DbContext with InMemory database
        services.AddDbContext<MicroErpDbContext>(options =>
            options.UseInMemoryDatabase("MicroErpDb"));

        // Register services
        services.AddScoped<CustomerService>();
        services.AddScoped<OrderService>();
        services.AddScoped<PcbService>();
        services.AddScoped<GuidebookService>();
        services.AddScoped<ConfigurationService>();

        // Seed data on startup
        services.AddHostedService<MicroErpDataSeedingService>();
    }

    public void RegisterNavigation(INavigationBuilder builder)
    {
        builder.AddNavItem("MicroERP", Icons.Material.Filled.PrecisionManufacturing, "/microerp", 10);
        builder.AddNavItem("Zamówienia", Icons.Material.Filled.Assignment, "/microerp/orders", 11);
        builder.AddNavItem("Klienci", Icons.Material.Filled.People, "/microerp/customers", 12);
        builder.AddNavItem("Płytki PCB", Icons.Material.Filled.Memory, "/microerp/pcbs", 13);
        builder.AddNavItem("Karty pracy", Icons.Material.Filled.BuildCircle, "/microerp/guidebooks", 14);
        builder.AddNavItem("Konfiguracja", Icons.Material.Filled.Settings, "/microerp/config", 15);
    }
}

/// <summary>
/// Background service that seeds the database on startup
/// </summary>
public class MicroErpDataSeedingService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public MicroErpDataSeedingService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MicroErpDbContext>();
        await context.Database.EnsureCreatedAsync(stoppingToken);
        MicroErpDataSeeder.SeedData(context);
    }
}