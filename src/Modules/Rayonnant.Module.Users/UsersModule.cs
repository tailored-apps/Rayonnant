using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using Rayonnant.Core.Modularity;

namespace Rayonnant.Module.Users;

public class UsersModule : IModule
{
    public ModuleInfo Info => new("Users", "User management module", Icons.Material.Filled.People, "/users", 2);
    public void ConfigureServices(IServiceCollection services) { }
    public void RegisterNavigation(INavigationBuilder builder)
        => builder.AddNavItem("Users", Icons.Material.Filled.People, "/users", 2);
}
