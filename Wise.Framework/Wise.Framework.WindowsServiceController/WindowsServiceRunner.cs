using System;
using System.Collections.Generic;
using Wise.Framework.DependencyInjection.Unity;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;

namespace Wise.Framework.WindowsServiceController
{
    public class WindowsServiceRunner : IDisposable
    {
        private static readonly IContainer container = new UnityContainerAdapter();
      
        public void RegisterService<TService>() where TService : AbstractServiceBase
        {
            container.RegisterType<AbstractServiceBase, TService>(LifetimeScope.Singleton, typeof(TService).Name);
        }

        public IContainer Container { get { return container;} }

        public void RunRegisteredServices(string[] args)
        {
            container.RegisterType<IWindowsServiceController, WindowsServiceController>(LifetimeScope.Singleton);

            using (var windowsServiceController = container.Resolve<IWindowsServiceController>())
            {
                windowsServiceController.RunModule(args);
            }
        }


        public void Dispose()
        {
           container.Dispose();
        }
    }

   
}