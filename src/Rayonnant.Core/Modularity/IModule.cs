using Microsoft.Extensions.DependencyInjection;

namespace Rayonnant.Core.Modularity;

public interface IModule
{
    ModuleInfo Info { get; }
    void ConfigureServices(IServiceCollection services);
    void RegisterNavigation(INavigationBuilder builder);
}
