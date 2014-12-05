using Wise.Framework.DependencyInjection.Unity;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;

namespace Wise.Framework.WindowsServiceController
{
    public static class WindowsServiceRunner
    {
        public static void Run(string[] args)
        {
            using (IContainer container = new UnityContainerAdapter())
            {
                container.RegisterType<IWindowsServiceController, WindowsServiceController>(LifetimeScope.Singleton);
               
                using (var windowsServiceController = container.Resolve<IWindowsServiceController>())
                {
                    windowsServiceController.RunModule(args);
                }
            }
        }
    }
}