using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Common.Logging;
using Wise.Framework.Bootstrapping;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Modularity;
using Wise.Framework.Interface.Window;
using Wise.Framework.Presentation.Interface.ViewModel;
using Wise.Framework.Presentation.Modularity;

namespace Wise.Framework.HostShell
{
    /// <summary>
    /// Default Implementation for Bootstrapping class which is responsible for running application and adding new 
    /// modules and starting them in proper way.
    /// </summary>
    public class BootsraperImpl : AbstractBootstrapper
    {
        /// <summary>
        /// contains logger
        /// </summary>
        private readonly ILog logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootsraperImpl" /> class.
        /// </summary>
        /// <param name="logger">logger which will be used</param>
        public BootsraperImpl(ILog logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Creates Module Catalog per application 
        /// </summary>
        /// <returns>module catalog</returns>
        public override IModuleCatalog CreateModuleCatalog()
        {
            logger.Info("Going to create Module Catalog");
            return new ModuleCatalog();
        }

        /// <summary>
        /// Method responsible for configuration module catalog
        /// </summary>
        /// <param name="catalog">catalog which will be configured.</param>
        public override void ConfigureModuleCatalog(IModuleCatalog catalog)
        {

            string path = @"Wise.DummyModule.dll";
            Assembly assembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path));
            Type type = assembly.GetType("Wise.DummyModule.DummyModule");
            catalog.AddModule(type.Name,type.AssemblyQualifiedName);
            
            string path2 = @"Wise.DummyModuleTwo.dll";
            Assembly assembly2 = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path2));
            Type type2 = assembly2.GetType("Wise.DummyModuleTwo.DummyModuleTwo");
            catalog.AddModule(type2.Name,type2.AssemblyQualifiedName);
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
        }

        public override void ConfigureAppliactionSplashInfo(ISplashViewModel splashViewModel)
        {
            splashViewModel.ProductName = "Wise Test WPF Application"; 
            splashViewModel.EnviormentName = Environment.MachineName;
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            splashViewModel.Version = fvi.FileVersion;
            splashViewModel.Logo = new Uri(@"pack://application:,,,/Wise.Framework.Presentation.Resources;component/Resources/logo.png", UriKind.Absolute);
        
        }
    }
}