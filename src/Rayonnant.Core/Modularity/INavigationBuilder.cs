namespace Rayonnant.Core.Modularity;

/// <summary>
/// Builder used by modules to register navigation entries displayed in the shell sidebar.
/// </summary>
public interface INavigationBuilder
{
    /// <summary>Add a navigation link to the shell sidebar.</summary>
    /// <param name="title">Display text shown next to the icon.</param>
    /// <param name="icon">MudBlazor Material icon identifier (e.g. <c>Icons.Material.Filled.Dashboard</c>).</param>
    /// <param name="href">Route path the link navigates to.</param>
    /// <param name="order">Sort order — lower values appear first.</param>
    void AddNavItem(string title, string icon, string href, int order = 0);
}
