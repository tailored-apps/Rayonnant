using System.Collections.Generic;
using System.Security.Principal;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;
using Wise.Framework.Interface.Environment;
using Wise.Framework.Interface.Security;
using Wise.Framework.Security.Providers;

namespace Wise.Framework.Security.Authentication
{
    public class SecurityService : ISecurityService
    {
        private IEnvironmentService environmentService;
        private readonly BaseSecurityProvider securityProvider;
        public SecurityService(IEnvironmentService environmentService, IContainer container)
        {
            this.environmentService = environmentService;
            if (container.IsTypeRegistered<BaseSecurityProvider>() && !environmentService.GetEnvironmentInfo().SelfContained)
            {
                this.securityProvider = container.Resolve<BaseSecurityProvider>();
            }
            else
            {
                container.RegisterTypeIfMissing<BaseSecurityProvider, WindowsSecurityProvider>(LifetimeScope.Singleton);
                securityProvider = container.Resolve<BaseSecurityProvider>();
            }
        }

        public IIdentity User
        {
            get { return securityProvider.GetCurrentIddentity(); }
        }

        public bool IsInRole(string roleName)
        {
            return securityProvider.IsInRole(roleName);
        }

        public IEnumerable<IRole> GetUserRoles(IIdentity user)
        {
            return securityProvider.GetRoles(user);
        }
    }
}