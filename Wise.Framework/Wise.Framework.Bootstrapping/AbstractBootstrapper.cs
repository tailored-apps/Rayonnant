using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Common.Logging;
using Microsoft.Practices.Prism.Modularity;
using Wise.Framework.Interface.Bootstrapping;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Window;
using Wise.Framework.Presentation.Window;
using IModuleCatalog = Wise.Framework.Interface.Modularity.IModuleCatalog;

namespace Wise.Framework.Bootstrapping
{
    /// <summary>
    ///     Class represents base abstract bootstrapping class.
    /// </summary>
    public abstract class AbstractBootstrapper : IBootstrapper
    {
        private readonly ILog logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        ///     Method responsible for registering logger inside application
        /// </summary>
        /// <param name="container">DI container</param>
        public virtual void RegisterLogger(IContainer container)
        {
        }

        /// <summary>
        ///     Abstract Method responsible for creating module catalog instance
        /// </summary>
        /// <returns>ModuleCatalog which will be used per application instance</returns>
        public abstract IModuleCatalog CreateModuleCatalog();

        /// <summary>
        ///     Method responsible for configuring container.
        /// </summary>
        /// <param name="container">DI Container</param>
        public virtual void ConfigureContainer(IContainer container)
        {
        }

        /// <summary>
        ///     Method which will be invoked after configuration container
        /// </summary>
        /// <param name="container">container object</param>
        /// <param name="messanger">messenger object</param>
        public virtual void PostConfiguration(IContainer container, IMessanger messanger)
        {
        }

        /// <summary>
        ///     Method which will register shell window in container
        /// </summary>
        /// <param name="container">DI Container</param>
        public virtual void RegisterShell(IContainer container)
        {
            container.RegisterTypeIfMissing<IShellWindow, ShellWindow>(LifetimeScope.Singleton);
        }

        /// <summary>
        ///     Method which is responsible for configuring shell window
        /// </summary>
        /// <param name="container">the container</param>
        /// <param name="shellWindow">shell window</param>
        public virtual void ConfigureShell(IContainer container, IShellWindow shellWindow)
        {
        }

        /// <summary>
        ///     Method invoked after module initialization
        /// </summary>
        /// <param name="container">container object</param>
        public virtual void PostModuleInitialization(IContainer container)
        {
        }

        /// <summary>
        ///     Method responsible for configuring module catalog
        /// </summary>
        /// <param name="catalog">module catalog</param>
        public virtual void ConfigureModuleCatalog(IModuleCatalog catalog)
        {
            var modulesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules");

            var modules = new DirectoryInfo(modulesPath).GetDirectories();
            var dlls = new DirectoryInfo(modulesPath).GetFiles("*.dll").ToList();
            foreach (var directoryInfo in modules)
            {
                var sub = directoryInfo.GetFiles("*.dll");
                dlls.AddRange(sub);
            }
            LoadModule(catalog, dlls);

        }

        private void LoadModule(IModuleCatalog catalog, IEnumerable<FileInfo> dlls)
        {
            foreach (var fileInfo in dlls)
            {
                try
                {
                    var assembly2 = Assembly.LoadFrom(fileInfo.FullName);
                    var types = assembly2.GetTypes();
                    foreach (var type in types)
                    {
                        if (type.GetInterfaces().Contains(typeof(IModule)))
                        {
                            logger.InfoFormat("Going To load module {0}", fileInfo.FullName);
                            LoadModule(catalog, type, fileInfo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.ErrorFormat("Could not load {0}", ex, fileInfo.FullName);
                }
            }
        }

        /// <summary>
        ///     Method responsible for setting up aplication splash screen
        /// </summary>
        /// <param name="splashViewModel">view model contains</param>
        public virtual void ConfigureAppliactionSplashInfo(ISplashViewModel splashViewModel)
        {
        }



        protected void LoadModule(IModuleCatalog catalog, Type type, FileInfo moduleFile)
        {
            catalog.AddModule(type.Name, type.AssemblyQualifiedName, new Uri(moduleFile.FullName, UriKind.RelativeOrAbsolute));
        }
    }
}