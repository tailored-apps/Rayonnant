using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using Rayonnant.Core.Modularity;

namespace Rayonnant.Module.Monitoring;

public class MonitoringModule : IModule
{
    public ModuleInfo Info => new("Monitoring", "System monitoring and health", Icons.Material.Filled.Monitor, "/monitoring", 3);
    public void ConfigureServices(IServiceCollection services) { }
    public void RegisterNavigation(INavigationBuilder builder)
        => builder.AddNavItem("Monitoring", Icons.Material.Filled.Monitor, "/monitoring", 3);
}
