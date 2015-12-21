using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.DependencyInjection
{
    public class UnityContainerServiceLocator : IServiceLocator
    {
        private readonly IContainer container;
        public UnityContainerServiceLocator(IContainer container)
        {
            this.container = container;
        }

        public object GetInstance(Type serviceType)
        {
            return container.Resolve(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return container.Resolve(serviceType, key);
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.ResolveAll(serviceType);
        }

        public TService GetInstance<TService>()
        {
            return container.Resolve<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            return container.Resolve<TService>(key);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return container.ResolveAll<TService>();
        }

        public object GetService(Type serviceType)
        {
            return container.Resolve(serviceType);
        }
    }
}
