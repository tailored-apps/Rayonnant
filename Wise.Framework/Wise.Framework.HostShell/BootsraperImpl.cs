using System;
using System.Collections.ObjectModel;
using System.IO;
using Common.Logging;
using Wise.Framework.Bootstrapping;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;
using Wise.Framework.Interface.ExceptionHandling;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Modularity;
using Wise.Framework.Interface.Window;
using Wise.Framework.Presentation;
using Wise.Framework.Presentation.Commands;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Interface.ViewModel;
using Wise.Framework.Presentation.Modularity;
using Wise.Framework.Presentation.Preferences;
using Wise.Framework.Presentation.Window;
namespace Wise.Framework.HostShell
{
    /// <summary>
    ///     Default Implementation for Bootstrapping class which is responsible for running application and adding new
    ///     modules and starting them in proper way.
    /// </summary>
    public class BootsraperImpl : AbstractBootstrapper
    {
        /// <summary>
        ///     contains logger
        /// </summary>
        private readonly ILog logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BootsraperImpl" /> class.
        /// </summary>
        /// <param name="logger">logger which will be used</param>
        public BootsraperImpl(ILog logger)
        {
            this.logger = logger;
        }

        /// <summary>
        ///     Creates Module Catalog per application
        /// </summary>
        /// <returns>module catalog</returns>
        public override IModuleCatalog CreateModuleCatalog()
        {
            logger.Info("Going to create Module Catalog");
            return new ModuleCatalog();
        }

        public override void ConfigureContainer(IContainer container)
        {
            base.ConfigureContainer(container);
            container.RegisterType<IExceptionService, ExceptionWindow>(LifetimeScope.Factory);
            container.RegisterType<IPreferenceProvider, RegistryPreferenceProvider>(LifetimeScope.Singleton);
            container.RegisterType<IPreferenceManager, PreferenceManager>(LifetimeScope.Singleton);
        }

        public override void ConfigureModuleCatalog(IModuleCatalog catalog)
        {
            LoadModule(catalog, typeof(DummyModule.DummyModule), new FileInfo(@".\Wise.DummyModule.dll"));
            LoadModule(catalog, typeof(DummyModuleTwo.DummyModuleTwo), new FileInfo(@".\Wise.DummyModuleTwo.dll"));
        }

        public override void PostConfiguration(IContainer container, IMessanger messanger)
        {
            // lets setup main window name and icon
            if (container.IsTypeRegistered<IShellViewModel>())
            {
                var shellViewModel = container.Resolve<IShellViewModel>();
                shellViewModel.Icon = new Uri("pack://application:,,,/Wise.Framework.Presentation.Resources;component/Resources/1389141962_229117.ico", UriKind.Absolute);
                shellViewModel.Title = "Wise Test WPF Application";
            }



            // lets setup status buttons and icons
            if (container.IsTypeRegistered<IStatusViewModel>())
            {
                var statusViewModel = container.Resolve<IStatusViewModel>();
                statusViewModel.MenuGroups = new ObservableCollection<MenuGroup>(new[]
                {
                    new HomeMenuGroup {ElementName="_Home",DataTemplateKey = "HomeMenuGroupDataTemplate" ,Command = new NavigateToHomeCommand()},
                    new OpenItemsMenuGroup {ElementName="_Open Items",DataTemplateKey = "OpenItesmMenuGroupDataTemplate",Command = new NavigateToOpenItemsCommand()},
                    new MenuGroup {ElementName="_Actions",DataTemplateKey = "ActionsMenuGroupDataTemplate"},
                    new MenuGroup {ElementName="_Financials",DataTemplateKey = "FinancialsMenuGroupDataTemplate"},
                    new MenuGroup {ElementName="_Tools",DataTemplateKey = "ToolsMenuGroupDataTemplate"},
                    new MenuGroup {ElementName="_Settings",DataTemplateKey = "SettingsMenuGroupDataTemplate"}
                });
            }
        }

        public override void ConfigureAppliactionSplashInfo(ISplashViewModel splashViewModel)
        {
            splashViewModel.ProductName = WiseApplication.Name;
            splashViewModel.EnviormentName = Environment.MachineName;
            splashViewModel.Version = WiseApplication.Version;
            splashViewModel.Logo = new Uri(@"pack://application:,,,/Wise.Framework.Presentation.Resources;component/Resources/logo.png", UriKind.Absolute);

        }
    }
}