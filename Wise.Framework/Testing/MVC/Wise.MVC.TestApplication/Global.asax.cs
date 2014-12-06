using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;
using Wise.Framework.Web;
using Wise.MVC.TestApplication.Controllers;

namespace Wise.MVC.TestApplication
{
    public class MvcApplication : WiseMvcApplication
    {

        protected override void InitializeAreas()
        {
            AreaRegistration.RegisterAllAreas();
        }
        protected override void InitializeFilters()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected override void InitializeRoute()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override void InitializeBundle()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override void RegisterServices(IContainer container)
        {
            container.RegisterType<IDupa, Dupa>(LifetimeScope.Factory);
            
        }
    }
}
