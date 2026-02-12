# Rayonnant v2 — Blazor Server Migration Plan

## Overview

Migrate Rayonnant from WPF/Prism (.NET 5, Windows-only) to Blazor Server (.NET 10, cross-platform) while preserving the MVVM pattern and modular architecture.

## Current Architecture (v1)

- **UI:** WPF + XAML
- **MVVM:** Prism (`BindableBase`, `DelegateCommand`, Regions)
- **DI:** Unity container with custom `IContainer` abstraction
- **Message Bus:** Custom `IMessanger` (pub/sub with type + optional key routing)
- **Modularity:** Prism `IModule` + filesystem scanning (`Modules/` folder)
- **Data:** ADO.NET providers + NHibernate, custom `Repository<T>`
- **Security:** Active Directory provider, role-based auth
- **Platform:** Windows-only (.NET 5)

## Target Architecture (v2)

- **UI:** Blazor Server + MudBlazor (3-column layout)
- **MVVM:** CommunityToolkit.Mvvm (`ObservableObject`, `RelayCommand`, `[ObservableProperty]`)
- **DI:** Microsoft.Extensions.DependencyInjection (native)
- **Message Bus:** MediatR (`INotification`, `INotificationHandler<T>`)
- **Modularity:** Custom `IModule` interface + DI service registration
- **Data:** EF Core (optional module, not required by shell)
- **Security:** ASP.NET Core Identity / Cookie-based auth
- **Real-time:** SignalR (built-in with Blazor Server)
- **Platform:** Cross-platform (.NET 10)

## Solution Structure

```
Rayonnant.Blazor/
├── src/
│   ├── Rayonnant.Core/                    # Interfaces + abstractions (no UI)
│   │   ├── Modularity/
│   │   │   ├── IModule.cs
│   │   │   ├── IModuleCatalog.cs
│   │   │   └── ModuleInfo.cs
│   │   ├── Messaging/
│   │   │   ├── IMessageBus.cs
│   │   │   └── Messages/
│   │   ├── Security/
│   │   │   └── IAuthService.cs
│   │   └── Navigation/
│   │       └── INavigationService.cs
│   │
│   ├── Rayonnant.Messaging/               # Message bus (MediatR)
│   │   └── MediatRMessageBus.cs
│   │
│   ├── Rayonnant.Data/                    # DAL module (optional, EF Core)
│   │   ├── DbContextBase.cs
│   │   └── Repository<T>.cs
│   │
│   ├── Rayonnant.Shell/                   # Host shell (Blazor Server app)
│   │   ├── Program.cs
│   │   ├── Components/
│   │   │   ├── Layout/
│   │   │   │   ├── MainLayout.razor
│   │   │   │   ├── NavMenu.razor
│   │   │   │   ├── AppBar.razor
│   │   │   │   └── SidePanel.razor
│   │   │   └── Shared/
│   │   │       └── ModuleHost.razor
│   │   ├── Services/
│   │   │   ├── ModuleLoader.cs
│   │   │   └── ShellViewModel.cs
│   │   └── wwwroot/
│   │
│   └── Modules/
│       └── Rayonnant.Module.Dashboard/
│           ├── DashboardModule.cs
│           ├── ViewModels/
│           └── Pages/
│
└── tests/
    └── Rayonnant.Tests/
```

## Layout

```
┌─────────────────────────────────────────────────────────┐
│  AppBar: Logo │ Search │ Navigation │ Account │ ⚙️      │
├────────┬────────────────────────────────┬───────────────┤
│        │                                │               │
│  Nav   │        Content Area            │  Side Panel   │
│  Menu  │      (Module Pages)            │  (Context)    │
│        │                                │               │
│        │                                │               │
├────────┴────────────────────────────────┴───────────────┤
│  Status Bar (optional)                                   │
└─────────────────────────────────────────────────────────┘
```

## Key Mappings (v1 → v2)

| v1 (WPF/Prism) | v2 (Blazor/MudBlazor) |
|-----------------|----------------------|
| `ViewModelBase` | `ObservableObject` (CommunityToolkit) |
| `DelegateCommand` | `RelayCommand` / `AsyncRelayCommand` |
| `IMessanger.Publish<T>()` | `IMediator.Publish(notification)` |
| `IMessanger.Subscribe<T>()` | `INotificationHandler<T>` |
| Prism `IModule` | Custom `IModule` with `ConfigureServices()` |
| Prism Regions | Blazor Router + `MudNavMenu` |
| Unity `IContainer` | `IServiceCollection` / `IServiceProvider` |
| XAML Views | Razor Components (`.razor`) |
| WPF `DataBinding` | Blazor `@bind` + ViewModel injection |
| NHibernate/ADO.NET | EF Core (optional module) |
| Active Directory | ASP.NET Core Identity |

## Migration Phases

### Phase 1 — Shell Skeleton ✅
- Solution structure with Core + Shell
- MudBlazor 3-column layout
- Basic cookie auth
- Module interface + example module

### Phase 2 — Message Bus + MVVM
- MediatR-based IMessageBus
- ViewModelBase with CommunityToolkit.Mvvm
- Wire ViewModels to Blazor components

### Phase 3 — Module Loader
- Dynamic module discovery
- Nav menu from module metadata
- Module lifecycle (init, dispose)

### Phase 4 — Port Existing Modules
- Security module → Blazor auth
- WPF Views → Razor components
- ViewModels (mostly 1:1)
