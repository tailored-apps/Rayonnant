using MudBlazor.Services;
using Rayonnant.Core.Messaging;
using Rayonnant.Core.Modularity;
using Rayonnant.Shell.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddSingleton<IMessageBus, MessageBus>();

// Register modules
var modules = new IModule[]
{
    new Rayonnant.Module.Dashboard.DashboardModule(),
    new Rayonnant.Module.Users.UsersModule(),
    new Rayonnant.Module.Monitoring.MonitoringModule(),
    new Rayonnant.Module.DataExplorer.DataExplorerModule(),
    new Rayonnant.Module.MicroErp.MicroErpModule()
};

foreach (var module in modules)
{
    builder.Services.AddSingleton<IModule>(module);
    module.ConfigureServices(builder.Services);
}

builder.Services.AddSingleton<ModuleLoader>();

var app = builder.Build();

// Initialize modules (nav registration)
var moduleLoader = app.Services.GetRequiredService<ModuleLoader>();
moduleLoader.Initialize();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<Rayonnant.Shell.Components.App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(
        typeof(Rayonnant.Module.Dashboard.DashboardModule).Assembly,
        typeof(Rayonnant.Module.Users.UsersModule).Assembly,
        typeof(Rayonnant.Module.Monitoring.MonitoringModule).Assembly,
        typeof(Rayonnant.Module.DataExplorer.DataExplorerModule).Assembly,
        typeof(Rayonnant.Module.MicroErp.MicroErpModule).Assembly);

app.Run();

public partial class Program { }
