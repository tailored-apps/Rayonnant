using Rayonnant.Core.Modularity;

namespace Rayonnant.Shell.Services;

public class ModuleLoader
{
    private readonly IEnumerable<IModule> _modules;
    private readonly List<NavItem> _navItems = [];

    public IReadOnlyList<NavItem> NavItems => _navItems;

    public ModuleLoader(IEnumerable<IModule> modules)
    {
        _modules = modules;
    }

    public void Initialize()
    {
        foreach (var module in _modules.OrderBy(m => m.Info.Order))
        {
            var navBuilder = new NavigationBuilder();
            module.RegisterNavigation(navBuilder);
            _navItems.AddRange(navBuilder.Items);
        }
    }
}
