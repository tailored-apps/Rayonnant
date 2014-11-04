using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.Web.DependencyInjection
{
    public class WiseDependencyResolver : IDependencyResolver, IDisposable
    {

        private readonly IContainer container;

        public WiseDependencyResolver(IContainer container)
        {
            this.container = container;
        }


        private const string HttpContextKey = "perRequestContainer";

        protected IContainer ChildContainer
        {
            get
            {
                var unityContainer = HttpContext.Current.Items[HttpContextKey] as IContainer;
                if (unityContainer == null)
                    HttpContext.Current.Items[HttpContextKey] = unityContainer = container.CreateChildContainer();
                return unityContainer;
            }
        }



        public object GetService(Type serviceType)
        {
            if (typeof(IController).IsAssignableFrom(serviceType))
                return ChildContainer.Resolve(serviceType);
            if (!IsRegistered(serviceType))
                return null;
            return ChildContainer.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (IsRegistered(serviceType))
                yield return ChildContainer.Resolve(serviceType);
            foreach (object obj in ChildContainer.ResolveAll(serviceType))
                yield return obj;
        }

        public static void DisposeOfChildContainer()
        {
            var unityContainer = HttpContext.Current.Items[HttpContextKey] as IContainer;
            if (unityContainer == null)
                return;
            unityContainer.Dispose();
        }

        private bool IsRegistered(Type typeToCheck)
        {
            var flag = true;
            if (typeToCheck.IsInterface || typeToCheck.IsAbstract)
            {
                flag = ChildContainer.IsTypeRegistered(typeToCheck);
                if (!flag && typeToCheck.IsGenericType)
                    flag = ChildContainer.IsTypeRegistered(typeToCheck.GetGenericTypeDefinition());
            }
            return flag;
        }

        public void Dispose()
        {
            DisposeOfChildContainer();
        }
    }
}
