namespace Rayonnant.Core.Modularity;

/// <summary>
/// Immutable descriptor for a loaded module — used by the shell for display and ordering.
/// </summary>
/// <param name="Name">Human-readable module name.</param>
/// <param name="Description">Short description shown in tooltips or admin panels.</param>
/// <param name="Icon">MudBlazor Material icon identifier.</param>
/// <param name="RoutePrefix">Default route prefix (e.g. <c>/dashboard</c>).</param>
/// <param name="Order">Sort order in the navigation menu — lower values appear first.</param>
public record ModuleInfo(string Name, string Description, string Icon, string RoutePrefix, int Order = 0);
