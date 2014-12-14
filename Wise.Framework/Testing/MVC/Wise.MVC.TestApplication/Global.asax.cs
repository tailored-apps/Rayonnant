using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Web;
namespace Wise.MVC.TestApplication
{
    public class MvcApplication : Wise.Framework.Web.WiseMvcApplication
    {
        protected override void InitializeFilters()
        {
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

        protected override void InitializeAreas()
        {
            AreaRegistration.RegisterAllAreas();
        }

        protected override void RegisterServices(IContainer container)
        {
            //container.RegisterType<>();
        }
    }
}
