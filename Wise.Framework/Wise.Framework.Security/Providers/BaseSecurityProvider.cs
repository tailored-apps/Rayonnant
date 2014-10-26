using System.Collections.Generic;
using System.Security.Principal;
using Wise.Framework.Interface.Security;

namespace Wise.Framework.Security.Providers
{
    public abstract class BaseSecurityProvider
    {
        public abstract IIdentity GetCurrentIddentity();

        public abstract  bool IsInRole(string roleName);

        public abstract IEnumerable<IRole> GetRoles(IIdentity user);
    }
}