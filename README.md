# Rayonnant v2

A modular application shell built with **Blazor Server** and **MudBlazor** — the successor to the WPF/Prism-based Rayonnant v1.

[![CI](https://github.com/tailored-apps/Rayonnant/actions/workflows/ci.yml/badge.svg?branch=v2)](https://github.com/tailored-apps/Rayonnant/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/TailoredApps.Rayonnant.Core)](https://www.nuget.org/packages/TailoredApps.Rayonnant.Core)

## What is Rayonnant?

Rayonnant is a **modular host shell** — a framework for building enterprise applications from independent, pluggable modules. Each module registers its own pages, services, and navigation entries. The shell provides the layout, theming, navigation, and inter-module messaging.

**v1** was WPF + Prism + Unity + NHibernate (`.NET 5`).  
**v2** is Blazor Server + MudBlazor + MediatR + EF Core (`.NET 10`).

## Tech Stack

| Layer | Technology | Version |
|-------|-----------|---------|
| UI | MudBlazor (dark theme) | 8.15 |
| Framework | Blazor Server (.NET 10) | 10.0 |
| DI | Native `Microsoft.Extensions.DependencyInjection` | — |
| Messaging | MediatR | 14.0 |
| MVVM helpers | CommunityToolkit.Mvvm | 8.4 |
| Testing | NUnit + Playwright + FluentAssertions | — |
| CI/CD | GitHub Actions → NuGet.org | — |

## Project Structure

```
Rayonnant/
├── src/
│   ├── Rayonnant.Core/              # 📦 NuGet package — contracts & abstractions
│   │   ├── Messaging/
│   │   │   └── IMessageBus.cs       # Publish/Send (wraps MediatR)
│   │   └── Modularity/
│   │       ├── IModule.cs           # Module contract: Info + ConfigureServices + RegisterNavigation
│   │       ├── INavigationBuilder.cs # Modules register nav items here
│   │       ├── ModuleInfo.cs        # Module metadata record
│   │       └── NavItem.cs           # Single sidebar nav entry
│   │
│   ├── Rayonnant.Shell/             # 🖥️ The host application (Blazor Server)
│   │   ├── Components/
│   │   │   ├── App.razor            # Root — sets InteractiveServer render mode
│   │   │   ├── Routes.razor         # Router with AdditionalAssemblies for modules
│   │   │   ├── Layout/
│   │   │   │   └── MainLayout.razor # MudBlazor 3-column layout: NavDrawer | Content | SidePanel
│   │   │   └── Pages/
│   │   │       ├── Home.razor       # Landing page
│   │   │       └── Settings.razor   # Shell settings
│   │   ├── Services/
│   │   │   ├── MessageBus.cs        # MediatR IMessageBus implementation
│   │   │   ├── ModuleLoader.cs      # Discovers and initializes all IModule registrations
│   │   │   └── NavigationBuilder.cs # Collects NavItems from modules
│   │   └── Program.cs              # DI setup, module registration, middleware
│   │
│   └── Modules/                     # 🧩 Example/dummy modules
│       ├── Rayonnant.Module.Dashboard/   # Welcome cards, stats, activity
│       ├── Rayonnant.Module.Users/       # User table, roles, timeline
│       ├── Rayonnant.Module.Monitoring/  # System health, services, alerts, logs
│       └── Rayonnant.Module.DataExplorer/ # SQL editor, results table, schema tree
│
├── tests/
│   └── Rayonnant.Tests/             # 🧪 Playwright E2E tests
│       ├── ShellLayoutTests.cs      # Layout verification + screenshots
│       ├── MonkeyTests.cs           # Random interaction stress tests
│       └── Support/
│           └── TestFixture.cs       # Server process manager
│
├── docs/
│   └── MIGRATION-PLAN.md           # v1→v2 migration analysis
│
├── .github/workflows/
│   └── ci.yml                      # Build → Test → Pack → Publish pipeline
│
└── Rayonnant.slnx                  # Solution file
```

## Quick Start

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Node.js (for Playwright browser install)

### Run the shell

```bash
cd src/Rayonnant.Shell
dotnet run
```

Open `https://localhost:5001` (or the URL shown in console).

### Run tests

```bash
# Install Playwright browsers (first time only)
pwsh tests/Rayonnant.Tests/bin/Release/net10.0/playwright.ps1 install chromium

# Run all tests
dotnet test -c Release
```

## Creating a Module

1. Create a Razor Class Library targeting `net10.0`
2. Reference `Rayonnant.Core` (or the NuGet package `TailoredApps.Rayonnant.Core`)
3. Implement `IModule`:

```csharp
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using Rayonnant.Core.Modularity;

public class MyModule : IModule
{
    public ModuleInfo Info => new(
        "My Module",
        "Does amazing things",
        Icons.Material.Filled.Star,
        "/my-module",
        Order: 10);

    public void ConfigureServices(IServiceCollection services)
    {
        // Register your services
        services.AddScoped<IMyService, MyService>();
    }

    public void RegisterNavigation(INavigationBuilder builder)
    {
        builder.AddNavItem("My Module", Icons.Material.Filled.Star, "/my-module", 10);
    }
}
```

4. Add a Razor page with `@page "/my-module"`
5. Register in the Shell's `Program.cs`:

```csharp
builder.Services.AddSingleton<IModule, MyModule>();

// And in MapRazorComponents:
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(MyModule).Assembly);
```

## Inter-Module Messaging

Modules communicate via `IMessageBus` (backed by MediatR):

```csharp
// Define a notification
public record UserCreated(string Name) : INotification;

// Publish from any module
await messageBus.PublishAsync(new UserCreated("Alice"));

// Handle in another module
public class UserCreatedHandler : INotificationHandler<UserCreated>
{
    public Task Handle(UserCreated notification, CancellationToken ct)
    {
        // React to the event
        return Task.CompletedTask;
    }
}
```

## Branching & Versioning

| Branch | Purpose | Version |
|--------|---------|---------|
| `release/v1` | v1 maintenance (WPF) | `1.0.x` |
| `v2` | Active v2 development | `2.0.x` |
| `develop` | Integration branch | — |
| `master` | Stable releases | — |
| `v*` tags | NuGet releases | exact version |

## CI/CD Pipeline

Every push to `v2`, `develop`, or `master` triggers:

1. **Build** — .NET 10, version injected from branch/tag
2. **Test** — 14+ Playwright E2E tests (layout, module pages, monkey tests)
3. **Pack** — `TailoredApps.Rayonnant.Core` NuGet package
4. **Publish** — to nuget.org (only on `v*` tags)

## Migration from v1

See [`docs/MIGRATION-PLAN.md`](docs/MIGRATION-PLAN.md) for the full analysis.

| v1 (WPF) | v2 (Blazor) |
|-----------|-------------|
| Prism `IModule` | `Rayonnant.Core.Modularity.IModule` |
| Unity DI | Native `IServiceCollection` |
| Custom `IMessanger` | MediatR `IMessageBus` |
| XAML DataTemplates | Razor components |
| NHibernate | EF Core |
| WPF NavigationRegion | Blazor Router + `INavigationBuilder` |

## License

MIT — © Tailored Apps
