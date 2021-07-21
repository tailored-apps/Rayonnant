using System.Collections.Generic;
using System.Security.Principal;
using TailoredApps.Rayonnant.Interface.DependencyInjection;
using TailoredApps.Rayonnant.Interface.DependencyInjection.Enum;
using TailoredApps.Rayonnant.Interface.Environment;
using TailoredApps.Rayonnant.Interface.Security;
using TailoredApps.Rayonnant.Security.Providers;

namespace TailoredApps.Rayonnant.Security.Authentication
{
    public class SecurityService : ISecurityService
    {
        private readonly BaseSecurityProvider securityProvider;

        public SecurityService(IEnvironmentService environmentService, IContainer container)
        {
            if (container.IsTypeRegistered<BaseSecurityProvider>() && !environmentService.GetEnvironmentInfo().SelfContained)
            {
                securityProvider = container.Resolve<BaseSecurityProvider>();
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