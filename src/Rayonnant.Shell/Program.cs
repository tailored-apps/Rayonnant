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
builder.Services.AddSingleton<ModuleLoader>();

// Register modules
builder.Services.AddSingleton<IModule, Rayonnant.Module.Dashboard.DashboardModule>();

var app = builder.Build();

// Initialize modules
var moduleLoader = app.Services.GetRequiredService<ModuleLoader>();
moduleLoader.Initialize(builder.Services);

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<Rayonnant.Shell.Components.App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(Rayonnant.Module.Dashboard.DashboardModule).Assembly);

app.Run();

public partial class Program { }
