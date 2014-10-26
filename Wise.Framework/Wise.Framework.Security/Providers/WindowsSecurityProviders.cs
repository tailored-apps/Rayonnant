using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Wise.Framework.Interface.Security;

namespace Wise.Framework.Security.Providers
{
    public class WindowsSecurityProvider : BaseSecurityProvider
    {
        public override IIdentity GetCurrentIddentity()
        {
            return WindowsIdentity.GetCurrent();
        }

        public override bool IsInRole(string roleName)
        {
            return WindowsPrincipal.Current.IsInRole(roleName);
        }

        public override IEnumerable<IRole> GetRoles(IIdentity user)
        {
            return WindowsIdentity.GetCurrent().Groups.Select(x => Role.Create(x.Value));

        }

    }
}
