using Microsoft.Extensions.DependencyInjection;

namespace Rayonnant.Core.Modularity;

/// <summary>
/// Contract for a Rayonnant plug-in module.
/// Each module provides metadata, registers its own services, and declares navigation entries.
/// </summary>
public interface IModule
{
    /// <summary>Module metadata (name, description, icon, default route, sort order).</summary>
    ModuleInfo Info { get; }

    /// <summary>Register module-specific services into the DI container.</summary>
    /// <param name="services">The application service collection.</param>
    void ConfigureServices(IServiceCollection services);

    /// <summary>Register navigation items so the shell can render them in the sidebar.</summary>
    /// <param name="builder">Navigation builder provided by the shell.</param>
    void RegisterNavigation(INavigationBuilder builder);
}
