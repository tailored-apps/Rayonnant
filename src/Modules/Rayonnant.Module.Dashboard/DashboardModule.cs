using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using Rayonnant.Core.Modularity;

namespace Rayonnant.Module.Dashboard;

public class DashboardModule : IModule
{
    public ModuleInfo Info => new("Dashboard", "Main dashboard module", Icons.Material.Filled.Dashboard, "/", 0);

    public void ConfigureServices(IServiceCollection services) { }

    public void RegisterNavigation(INavigationBuilder builder)
    {
        builder.AddNavItem("Dashboard", Icons.Material.Filled.Dashboard, "/", 0);
    }
}
