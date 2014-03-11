

using Microsoft.Practices.Prism.Modularity;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Window;

namespace Wise.Framework.Interface.Bootstrapping
{
    /// <summary>
    /// Interface which is used by bootstrapping classes
    /// </summary>
    public  interface IBootstrapper
    {
        /// <summary>
        /// used to configure module catalog for prism
        /// </summary>
        /// <param name="catalog">catalog used to config</param>
        void ConfigureModuleCatalog(ModuleCatalog catalog);

        /// <summary>
        /// method for registering container
        /// </summary>
        /// <param name="container">container</param>
        void RegisterLogger(IContainer container);
        
        /// <summary>
        /// method which allows to crate module catalog
        /// </summary>
        /// <returns><see cref="IModuleCatalog"/></returns>
        ModuleCatalog CreateModuleCatalog();
        
        /// <summary>
        /// Method for container configuration
        /// </summary>
        /// <param name="container">container</param>
        void ConfigureContainer(IContainer container);
        
        /// <summary>
        /// method invoked after configuratioon container
        /// </summary>
        /// <param name="container">container</param>
        /// <param name="messanger">messanger sysytem </param>
        void PostConfiguration(IContainer container, IMessanger messanger);
        
        /// <summary>
        /// Method for registering shell window
        /// </summary>
        /// <param name="container">container where shell will be registered</param>
        void RegisterShell(IContainer container);
        
        /// <summary>
        /// method used for configuration shell window
        /// </summary>
        /// <param name="container">container</param>
        /// <param name="shellWindow">shell window to configure</param>
        void ConfigureShell(IContainer container, IShellWindow shellWindow);
        
        /// <summary>
        /// method invoked after module init. 
        /// </summary>
        /// <param name="container">container</param>
        void PostModuleInitialization(IContainer container);

        void ConfigureAppliactionSplashInfo(ISplashViewModel splashViewModel);

    }
}
