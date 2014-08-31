using System;
using System.Windows.Controls;
using Common.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Wise.Framework.DependencyInjection.Unity;
using Wise.Framework.Environment;
using Wise.Framework.Interface.Bootstrapping;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;
using Wise.Framework.Interface.Environment;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Modularity;
using Wise.Framework.Interface.Security;
using Wise.Framework.Interface.Window;
using Wise.Framework.InternalMessagning;
using Wise.Framework.Logging;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Interface.Menu;
using Wise.Framework.Presentation.Interface.Modularity;
using System.Windows;
using Microsoft.Practices.Prism.Logging;

using Microsoft.Practices.ServiceLocation;
using Wise.Framework.Presentation.Interface.ViewModel;
using Wise.Framework.Presentation.Modularity;
using Wise.Framework.Presentation.Services;
using Wise.Framework.Presentation.ViewModel;
using Wise.Framework.Presentation.Window;
using Wise.Framework.Security.Authentication;

namespace Wise.Framework.Bootstrapping
{
    public class BootstrapperRunner : IBootstrapperRunner
    {
        /// <summary>
        ///     the shell window
        /// </summary>
        private IShellWindow shellWindow;

        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Creates instance of BootstrapperRunner
        /// </summary>
        public BootstrapperRunner()
        {
            Container = new UnityContainerAdapter();
        }

        /// <summary>
        ///     Gets Bootstrap class
        /// </summary>
        private IBootstrapper Bootstrapper { get; set; }

        /// <summary>
        ///     Gets  Messenger object
        /// </summary>
        private IMessanger Messanger { get; set; }

        /// <summary>
        ///     Gets or Sets ModuleCatalog.
        /// </summary>
        private ModuleCatalog ModuleCatalog { get; set; }

        /// <summary>
        ///     Gets or sets Container
        /// </summary>
        private IContainer Container { get; set; }


        /// <summary>
        ///     Method responsible for running.
        /// </summary>
        /// <param name="bootstrapper">bootstrapping class object</param>
        public void Run(IBootstrapper bootstrapper)
        {
            log.Debug("going to run bootstrapping class");
            Bootstrapper = bootstrapper;

            ConfigureModuleCatalog();

            ConfigureContainer();
            ConfigureApplicationInformation();
            ShowSplashScreen();
            PublishSystemMessage("Splash screen opened");
            AuthenticateUser();
            InitialiseShell();

            if (Container.IsTypeRegistered<IModuleManager>())
            {
                log.Debug("module manager is registered going to initialize modules");
                InitializeModules();
                Bootstrapper.PostModuleInitialization(Container);
            }

            log.Debug("going to close splash screen");
            
            CloseSplashScreen();
            PublishSystemMessage("Application Started :-)");
        }

        private void PublishSystemMessage(string messageToSend)
        {
            Messanger.Publish(new SystemNotyficationMessage
            {
                Message = messageToSend
            });
        }

        private void ConfigureApplicationInformation()
        {
            if (Container.IsTypeRegistered<ISplashViewModel>())
            {
                Bootstrapper.ConfigureAppliactionSplashInfo(Container.Resolve<ISplashViewModel>());
            }
        }

        /// <summary>
        ///     method responsible for closing splash screen
        /// </summary>
        protected virtual void CloseSplashScreen()
        {
            if (!Container.IsTypeRegistered<ISplashViewModel>())
                return;
            Container.Resolve<ISplashRunner>().CloseSplash();
        }

        /// <summary>
        ///     Method initializes modules
        /// </summary>
        protected virtual void InitializeModules()
        {
            log.Debug("about registering module manager");
            IModuleManager moduleManager;
            try
            {
                moduleManager = Container.Resolve<IModuleManager>();
                log.Info("module manager resolved");
            }
            catch (Exception ex)
            {
                log.Warn("module manager has been not resolved.");
                log.Error(ex.Message, ex);
                throw;
            }
            log.Debug("going to run module manager.");
            moduleManager.Run();
        }


        /// <summary>
        ///     Method creates module catalog
        /// </summary>
        /// <returns></returns>
        protected virtual ModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = Bootstrapper.CreateModuleCatalog();

            if (moduleCatalog != null)
            {


                log.Info("using registered module catalog from Bootstrapping class.");
            }
            else
            {
                log.Info("Creating default module catalog");
            }

            return moduleCatalog ?? new ModuleCatalog();
        }

        /// <summary>
        ///     Method responsible for initializing shell
        /// </summary>
        protected virtual void InitialiseShell()
        {
            PublishSystemMessage("Going to init shell window");
            log.Debug("about initializing shell");

            PublishSystemMessage("post initialization has been completed");
            var regionManager = Container.Resolve<IRegionManager>();

            PublishSystemMessage("Going to setup regions on shell window");
            if (Container.IsTypeRegistered<ICommandsViewModel>())
            {
                log.Info("registering CommandRegion in shell");
                regionManager.RegisterViewWithRegion(ShellRegionNames.CommandRegion, Container.Resolve<ICommandsViewModel>);

            }

            if (Container.IsTypeRegistered<IStatusViewModel>())
            {
                log.Info("registering StatusRegion in shell");
                regionManager.RegisterViewWithRegion(ShellRegionNames.StatusRegion, Container.Resolve<IStatusViewModel>);
            }



            PublishSystemMessage("regions has been registerd");
            Bootstrapper.RegisterShell(Container);

            PublishSystemMessage("Going to register shell window");
            Container.RegisterTypeIfMissing<IShellWindow, ShellWindow>(LifetimeScope.Singleton);

            shellWindow = Container.Resolve<IShellWindow>();

            PublishSystemMessage("Going to configure shell window");
            Bootstrapper.ConfigureShell(Container, shellWindow);

            PublishSystemMessage("Going to post init application");
            Bootstrapper.PostConfiguration(Container, Messanger);

            var shell = shellWindow as Window;
            if (shell != null)
            {
                log.Info("going to register shell view to shell window");
                shell.DataContext = Container.Resolve<IShellViewModel>();
                var regionCfg = Container.Resolve<IRegionConfigurator>();
                log.Info("going to configure and register regions");
                regionCfg.ConfigureRegions();
                regionCfg.InitializeShell(shell, regionManager);

                Container.RegisterTypeIfMissing<INavigationManager, NavigationManager>(LifetimeScope.Singleton);
                if (Container.IsTypeRegistered<INavigationManager>())
                {
                    var navManager = Container.Resolve<INavigationManager>();

                }
                shell.Closing += (s, e) => { };

                if (Application.Current != null)
                {
                    PublishSystemMessage("Going to register main window");
                    Application.Current.MainWindow = shell;
                }

            }

            PublishSystemMessage("Going to show shell");
            shellWindow.Show();
            if (shell != null) shell.Activate();
        }

        /// <summary>
        ///     Method responsible for user authentication
        /// </summary>
        protected virtual void AuthenticateUser()
        {
            PublishSystemMessage("Going to authenticate user on aplication");
            PublishSystemMessage("user has been authenticated");
        }

        /// <summary>
        ///     method responsible for showing splash screen
        /// </summary>
        protected virtual void ShowSplashScreen()
        {
            log.Debug("about showing splash screen");
            if (!Container.IsTypeRegistered<ISplashViewModel>())
                return;
            Container.Resolve<ISplashRunner>().ShowSplash();
        }

        /// <summary>
        ///     method responsible for configuring container
        /// </summary>
        protected virtual void ConfigureContainer()
        {
            log.Debug("about configuring container");

            Container.RegisterTypeIfMissing<ISplashViewModel, SplashViewModel>(LifetimeScope.Singleton);

            Container.RegisterTypeIfMissing<ILoggerFacade, DefaultLoggerFacade>(LifetimeScope.Singleton);
            Container.RegisterTypeIfMissing<IServiceLocator, UnityServiceLocatorAdapter>(LifetimeScope.Singleton);
            ServiceLocator.SetLocatorProvider(() => Container.Resolve<IServiceLocator>());
            DependencyInjection.Container.Current = Container;

            Bootstrapper.ConfigureContainer(Container);
            Container.RegisterInstance(ModuleCatalog);

            Container.RegisterTypeIfMissing<IShellViewModel, ShellViewModel>(LifetimeScope.Singleton);
            Container.RegisterTypeIfMissing<ISecurityService, SecurityService>(LifetimeScope.Singleton);
            Container.RegisterTypeIfMissing<IEnvironmentService, EnvironmentService>(LifetimeScope.Singleton);
            Container.RegisterTypeIfMissing<ICommandsViewModel, CommandsViewModel>(LifetimeScope.Singleton);
            Container.RegisterTypeIfMissing<IMenuService, MenuService>(LifetimeScope.Singleton);
            Container.RegisterTypeIfMissing<IStatusViewModel, StatusViewModel>(LifetimeScope.Singleton);
            Container.RegisterTypeIfMissing<IProgressViewModel, ProgressViewModel>(LifetimeScope.Singleton);

            Container.RegisterTypeIfMissing<IEventAggregator, EventAggregator>(LifetimeScope.Singleton);

            Container.RegisterTypeIfMissing<IResourceManager, ResourceManager>(LifetimeScope.Singleton);
            Container.RegisterTypeIfMissing<IModuleManager, ModuleManager>(LifetimeScope.Singleton);
            Container.RegisterTypeIfMissing<IModuleInitializer, ModuleInitializer>(LifetimeScope.Singleton);

            Container.RegisterTypeIfMissing<IRegionConfigurator, RegionConfigurator>(LifetimeScope.Singleton);
            var regionConfigurator = Container.Resolve<IRegionConfigurator>();
            regionConfigurator.ConfigureContainer(Container);

            if (Container.IsTypeRegistered<IResourceManager>())
            {
                var resMang = Container.Resolve<IResourceManager>();
                resMang.MergeViewModelTemplates();
            }

            Container.RegisterTypeIfMissing<IMessanger, Messanger>(LifetimeScope.Singleton);
            Container.RegisterTypeIfMissing<IMessangerExecutor, MessangerExecutor>(LifetimeScope.Singleton);
            Container.RegisterTypeIfMissing<ISplashRunner, SplashRunner>(LifetimeScope.Singleton);



            if (Container.IsTypeRegistered<IMessanger>())
            {
                Messanger = Container.Resolve<IMessanger>();
            }




            var menu = Container.Resolve<IMenuService>();
            menu.AddMenuItem(new MenuItem() { Header = "Menu Item One" }, MenuService.MENU_ITEMS_PREFIX);
            menu.AddMenuItem(new MenuItem() { Header = "Menu Item Two" }, MenuService.MENU_ITEMS_PREFIX);
        }

        /// <summary>
        ///     method responsible for configuration module catalog.
        /// </summary>
        protected virtual void ConfigureModuleCatalog()
        {
            log.Debug("about configuring module catalog");
            ModuleCatalog = CreateModuleCatalog();

            log.Info("Module catalog created, Going to configure");
            Bootstrapper.ConfigureModuleCatalog(ModuleCatalog);
            log.Info("Module Catalog has been configured.");
            Container.RegisterInstance<IModuleCatalog>(ModuleCatalog);
        }

    }
}
