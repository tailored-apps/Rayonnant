namespace Rayonnant.Core.Modularity;

public interface INavigationBuilder
{
    void AddNavItem(string title, string icon, string href, int order = 0);
}
