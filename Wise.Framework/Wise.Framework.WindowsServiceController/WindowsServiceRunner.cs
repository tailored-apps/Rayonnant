using Wise.Framework.DependencyInjection.Unity;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.WindowsServiceController
{
    public static class WindowsServiceRunner
    {
        public static void Run(string[] args)
        {
            using (IContainer container = new UnityContainerAdapter())
            {
                using (IController controller = new WindowsServiceController(container))
                {
                    controller.RunModule(args);
                }
            }
        }
    }
}