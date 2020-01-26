using System;
using System.Windows;
using Common.Logging;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;
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
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Interface.Menu;
using Wise.Framework.Presentation.Interface.Modularity;
using Wise.Framework.Presentation.Interface.Shell;
using Wise.Framework.Presentation.Interface.ViewModel;
using Wise.Framework.Presentation.Logging;
using Wise.Framework.Presentation.Modularity;
using Wise.Framework.Presentation.Services;
using Wise.Framework.Presentation.ViewModel;
using Wise.Framework.Presentation.Window;
using Wise.Framework.Security.Authentication;
using IModuleCatalog = Wise.Framework.Interface.Modularity.IModuleCatalog;
using ModuleCatalog = Wise.Framework.Presentation.Modularity.ModuleCatalog;
using Prism.Events;
using Wise.Framework.DependencyInjection;
using CommonServiceLocator;

namespace Wise.Framework.Bootstrapping
{
    public class BootstrapperRunner : IBootstrapperRunner
    {
        private static readonly ILog Log = LogManager.GetLogger< BootstrapperRunner>();

        /// <summary>
        ///     the shell window
        /// </summary>
        private IShellWindow shellWindow;

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
        private IModuleCatalog ModuleCatalog { get; set; }

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
            Log.Debug("going to run bootstrapping class");
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
                Log.Debug("module manager is registered going to initialize modules");
                InitializeModules();
                Bootstrapper.PostModuleInitialization(Container);
            }

            Log.Debug("going to close splash screen");

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
            Log.Debug("about registering module manager");
            IModuleManager moduleManager;
            try
            {
                moduleManager = Container.Resolve<IModuleManager>();
                Log.Info("module manager resolved");
            }
            catch (Exception ex)
            {
                Log.Warn("module manager has been not resolved.");
                Log.Error(ex.Message, ex);
                throw;
            }
            Log.Debug("going to run module manager.");
            moduleManager.Run();
        }


        /// <summary>
        ///     Method creates module catalog
        /// </summary>
        /// <returns></returns>
        protected virtual IModuleCatalog CreateModuleCatalog()
        {
            IModuleCatalog moduleCatalog = Bootstrapper.CreateModuleCatalog();

            if (moduleCatalog != null)
            {
                Log.Info("using registered module catalog from Bootstrapping class.");
            }
            else
            {
                Log.Info("Creating default module catalog");
            }

            return moduleCatalog ?? new ModuleCatalog();
        }

        /// <summary>
        ///     Method responsible for initializing shell
        /// </summary>
        protected virtual void InitialiseShell()
        {
            PublishSystemMessage("Going to init shell window");
            Log.Debug("about initializing shell");

            PublishSystemMessage("post initialization has been completed");
            var regionManager = Container.Resolve<IRegionManager>();

            PublishSystemMessage("Going to setup regions on shell window");
            if (Container.IsTypeRegistered<ICommandsViewModel>())
            {
                Log.Info("registering CommandRegion in shell");
                regionManager.RegisterViewWithRegion(ShellRegionNames.CommandRegion,
                    Container.Resolve<ICommandsViewModel>);
            }

            if (Container.IsTypeRegistered<IStatusViewModel>())
            {
                Log.Info("registering StatusRegion in shell");
                regionManager.RegisterViewWithRegion(ShellRegionNames.StatusRegion, Container.Resolve<IStatusViewModel>);
            }
            if (Container.IsTypeRegistered<IMenuViewModel>())
            {
                Log.Info("registering left Menu region in shell");
                regionManager.RegisterViewWithRegion(ShellRegionNames.LeftSideNavigationRegion, Container.Resolve<IMenuViewModel>);
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
                Log.Info("going to register shell view to shell window");
                shell.DataContext = Container.Resolve<IShellViewModel>();
                var regionCfg = Container.Resolve<IRegionConfigurator>();
                Log.Info("going to configure and register regions");
                regionCfg.ConfigureRegions();
                regionCfg.InitializeShell(shell, regionManager);

                Container.RegisterTypeIfMissing<INavigationManager, NavigationManager>(LifetimeScope.Singleton);
                if (Container.IsTypeRegistered<INavigationManager>())
                {
                    var navManager = Container.Resolve<INavigationManager>();
                    navManager.RegisterTypeForNavigation<OpenItemsViewModel>();
                    navManager.RegisterTypeForNavigation<UserPreferencesSettingsViewModel>();
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
            if (shell != null)
            {
                shell.Activate();
            }
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
            Log.Debug("about showing splash screen");
            if (!Container.IsTypeRegistered<ISplashViewModel>())
                return;
            Container.Resolve<ISplashRunner>().ShowSplash();
        }

        /// <summary>
        ///     method responsible for configuring container
        /// </summary>
        protected virtual void ConfigureContainer()
        {
            Log.Debug("about configuring container");

            Container.RegisterTypeIfMissing<ISplashViewModel, SplashViewModel>(LifetimeScope.Singleton);
            Container.RegisterTypeIfMissing<ILoggerFacade, DefaultLoggerFacade>(LifetimeScope.Singleton);
            

            Container.RegisterTypeIfMissing<IServiceLocator, UnityContainerServiceLocator>(LifetimeScope.Singleton);
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
            Container.RegisterTypeIfMissing<IMenuViewModel, MenuViewModel>(LifetimeScope.Singleton);
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


            //if (Container.IsTypeRegistered<IMessanger>())
            //{
                Messanger = Container.Resolve<IMessanger>();
            //}
        }

        /// <summary>
        ///     method responsible for configuration module catalog.
        /// </summary>
        protected virtual void ConfigureModuleCatalog()
        {
            Log.Debug("about configuring module catalog");
            ModuleCatalog = CreateModuleCatalog();

            Log.Info("Module catalog created, Going to configure");
            Bootstrapper.ConfigureModuleCatalog(ModuleCatalog);
            Log.Info("Module Catalog has been configured.");
            var moduleCatalog = ModuleCatalog as ModuleCatalog;

            Container.RegisterInstance<Prism.Modularity.IModuleCatalog>(moduleCatalog);
        }
    }
}