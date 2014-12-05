using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.Web
{
    public class ControllerFactory : IControllerFactory
    {
        private IContainer container;
        public ControllerFactory(IContainer container)
        {
            this.container = container;

        }
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            return container.Resolve<IController>(controllerName + "Controller");
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            throw new NotImplementedException();
        }
    }
}
