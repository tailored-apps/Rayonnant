namespace Rayonnant.Core.Modularity;

/// <summary>
/// A single navigation entry rendered in the shell sidebar.
/// </summary>
/// <param name="Title">Display label.</param>
/// <param name="Icon">MudBlazor Material icon identifier.</param>
/// <param name="Href">Route the link navigates to.</param>
/// <param name="Order">Sort order — lower values appear first.</param>
public record NavItem(string Title, string Icon, string Href, int Order = 0);
