namespace Rayonnant.Core.Modularity;

public record ModuleInfo(string Name, string Description, string Icon, string RoutePrefix, int Order = 0);
