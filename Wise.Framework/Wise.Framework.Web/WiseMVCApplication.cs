using System.Web;
using System.Web.Mvc;
using Wise.Framework.DependencyInjection.Unity;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Web.DependencyInjection;

namespace Wise.Framework.Web
{
    public abstract class WiseMvcApplication : HttpApplication
    {


        private readonly IContainer container = new UnityContainerAdapter();
        protected void Application_Start()
        {
            RegisterServices(container);

            InitializeAreas();
            InitializeFilters();
            InitializeRoute();
            InitializeBundle();
            InitialiseDependencyInjection(container);
        }

        protected abstract void InitializeFilters();
        protected abstract void InitializeRoute();
        protected abstract void InitializeBundle();

        protected abstract void RegisterServices(IContainer container);
        protected abstract void InitializeAreas();

        protected virtual void InitialiseDependencyInjection(IContainer container)
        {
            DependencyResolver.SetResolver(new WiseDependencyResolver(container));

        }
        public override void Dispose()
        {
            base.Dispose();
            container.Dispose();

        }

    }
}
