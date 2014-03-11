using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Wise.Framework.DependencyInjection.Unity;
using Wise.Framework.Interface.Bootstrapping;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Window;

namespace Wise.Framework.Bootstrapping
{
    public class BootstrapperRunner: IBootstrapperRunner
    {
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
                InitializeModules();
                Bootstrapper.PostModuleInitialization(Container);
            }

            
            CloseSplashScreen();
            PublishSystemMessage("Application Started :-)");
        }

        private void PublishSystemMessage(string messageToSend)
        {
            //Messanger.Publish<SystemNotyficationMessage>(new SystemNotyficationMessage()
            //{
            //    Message = messageToSend
            //});
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
            //if (!Container.IsTypeRegistered<ISplashViewModel>())
            //    return;
            //Container.Resolve<ISplashRunner>().CloseSplash();
        }

        /// <summary>
        ///     Method initializes modules
        /// </summary>
        protected virtual void InitializeModules()
        {
             IModuleManager moduleManager;
            try
            {
                moduleManager = Container.Resolve<IModuleManager>();
                    }
            catch (Exception ex)
            {
                throw;
            }
            moduleManager.Run();
        }


        /// <summary>
        ///     Method creates module catalog
        /// </summary>
        /// <returns></returns>
        protected virtual ModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = Bootstrapper.CreateModuleCatalog();
            return moduleCatalog ?? new ModuleCatalog();
        }

        /// <summary>
        ///     Method responsible for initializing shell
        /// </summary>
        protected virtual void InitialiseShell()
        {
            //PublishSystemMessage("Going to init shell window");

            //PublishSystemMessage("post initialization has been completed");
            //var regionManager = Container.Resolve<IRegionManager>();

            //PublishSystemMessage("Going to setup regions on shell window");
            //if (Container.IsTypeRegistered<ICommandsViewModel>())
            //{
            //    regionManager.RegisterViewWithRegion(ShellRegionNames.CommandRegion, Container.Resolve<ICommandsViewModel>);
            //}

            //if (Container.IsTypeRegistered<IStatusViewModel>())
            //{
            //    regionManager.RegisterViewWithRegion(ShellRegionNames.StatusRegion, Container.Resolve<IStatusViewModel>);
            //}

            //PublishSystemMessage("regions has been registerd");
            //Bootstrapper.RegisterShell(Container);

            //PublishSystemMessage("Going to register shell window");
            //Container.RegisterTypeIfMissing<IShellWindow, ShellWindow>(LifetimeScope.Singleton);

            //INavigationManager manger = new NavigationManager(Messanger, regionManager);
            //Container.RegisterInstance(manger);

            //shellWindow = Container.Resolve<IShellWindow>();

            //PublishSystemMessage("Going to configure shell window");
            //Bootstrapper.ConfigureShell(Container, shellWindow);

            //PublishSystemMessage("Going to post init application");
            //Bootstrapper.PostConfiguration(Container, Messanger);

            //var shell = shellWindow as Window;
            //if (shell != null)
            //{
            //    shell.DataContext = Container.Resolve<IShellViewModel>();
            //    var regionCfg = Container.Resolve<IRegionConfigurator>();
            //    log.Info("going to configure and register regions");
            //    regionCfg.ConfigureRegions();
            //    regionCfg.InitializeShell(shell);

            //    shell.Closing += (s, e) => { };

            //    if (Application.Current != null)
            //    {
            //        PublishSystemMessage("Going to register main window");
            //        Application.Current.MainWindow = shell;
            //    }
            //    Application.Current.Dispatcher.VerifyAccess();
            //    Application.Current.Dispatcher.Invoke(new Action(() =>
            //    {
            //        PublishSystemMessage("Going to show shell");
            //        shellWindow.Show();
            //        shell.Activate();
            //    }));
            //}


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
            if (!Container.IsTypeRegistered<ISplashViewModel>())
                return;
            //Container.Resolve<ISplashRunner>().ShowSplash();
        }

        /// <summary>
        ///     method responsible for configuring container
        /// </summary>
        protected virtual void ConfigureContainer()
        {

            //Container.RegisterTypeIfMissing<ISplashViewModel, SplashViewModel>(LifetimeScope.Singleton);

            //Container.RegisterTypeIfMissing<ILoggerFacade, DefaultLoggerFacade>(LifetimeScope.Singleton);
            //Container.RegisterTypeIfMissing<IServiceLocator, UnityServiceLocatorAdapter>(LifetimeScope.Singleton);
            //ServiceLocator.SetLocatorProvider(() => Container.Resolve<IServiceLocator>());
            //DependencyInjection.Container.Current = Container;

            //Bootstrapper.ConfigureContainer(Container);
            //Container.RegisterInstance(ModuleCatalog);

            //Container.RegisterTypeIfMissing<IShellViewModel, ShellViewModel>(LifetimeScope.Singleton);
            //Container.RegisterTypeIfMissing<ISecurityService, SecurityService>(LifetimeScope.Singleton);
            //Container.RegisterTypeIfMissing<IEnvironmentService, EnvironmentService>(LifetimeScope.Singleton);
            //Container.RegisterTypeIfMissing<ICommandsViewModel, CommandsViewModel>(LifetimeScope.Singleton);
            //Container.RegisterTypeIfMissing<IStatusViewModel, StatusViewModel>(LifetimeScope.Singleton);
            //Container.RegisterTypeIfMissing<IProgressViewModel, ProgressViewModel>(LifetimeScope.Singleton);

            //Container.RegisterTypeIfMissing<IEventAggregator, EventAggregator>(LifetimeScope.Singleton);

            //Container.RegisterTypeIfMissing<IResourceManager, ResourceManager>(LifetimeScope.Singleton);
            //Container.RegisterTypeIfMissing<IModuleManager, ModuleManager>(LifetimeScope.Singleton);
            //Container.RegisterTypeIfMissing<IModuleInitializer, ModuleInitializer>(LifetimeScope.Singleton);

            //Container.RegisterTypeIfMissing<IRegionConfigurator, RegionConfigurator>(LifetimeScope.Singleton);
            //var regionConfigurator = Container.Resolve<IRegionConfigurator>();
            //regionConfigurator.ConfigureContainer(Container);

            //if (Container.IsTypeRegistered<IResourceManager>())
            //{
            //    var resMang = Container.Resolve<IResourceManager>();
            //    resMang.MergeViewModelTemplates();
            //}

            //Container.RegisterTypeIfMissing<IMessanger, Messanger>(LifetimeScope.Singleton);
            //Container.RegisterTypeIfMissing<IMessangerExecutor, MessangerExecutor>(LifetimeScope.Singleton);
            //Container.RegisterTypeIfMissing<ISplashRunner, SplashRunner>(LifetimeScope.Singleton);


            //if (Container.IsTypeRegistered<IMessanger>())
            //{
            //    Messanger = Container.Resolve<IMessanger>();
            //}
        }

        /// <summary>
        ///     method responsible for configuration module catalog.
        /// </summary>
        protected virtual void ConfigureModuleCatalog()
        {
            //log.Debug("about configuring module catalog");
            //ModuleCatalog = CreateModuleCatalog();

            //log.Info("Module catalog created, Going to configure");
            //Bootstrapper.ConfigureModuleCatalog(ModuleCatalog);
            //log.Info("Module Catalog has been configured.");
        }

    }
}
