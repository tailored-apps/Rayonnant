using Rayonnant.Core.Modularity;

namespace Rayonnant.Shell.Services;

public class NavigationBuilder : INavigationBuilder
{
    private readonly List<NavItem> _items = [];
    public IReadOnlyList<NavItem> Items => _items;

    public void AddNavItem(string title, string icon, string href, int order = 0)
        => _items.Add(new NavItem(title, icon, href, order));
}
