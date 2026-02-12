using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using Rayonnant.Core.Modularity;

namespace Rayonnant.Module.DataExplorer;

public class DataExplorerModule : IModule
{
    public ModuleInfo Info => new("Data Explorer", "Browse and query data", Icons.Material.Filled.TableChart, "/data", 4);
    public void ConfigureServices(IServiceCollection services) { }
    public void RegisterNavigation(INavigationBuilder builder)
        => builder.AddNavItem("Data Explorer", Icons.Material.Filled.TableChart, "/data", 4);
}
