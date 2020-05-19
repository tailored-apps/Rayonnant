using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            return WindowsIdentity.GetCurrent().Groups.Translate(typeof(NTAccount)).Select(x => Role.Create(x.Value)).Any(x=>x.Name==roleName);
        }

        public override IEnumerable<IRole> GetRoles(IIdentity user)
        {
            return WindowsIdentity.GetCurrent().Groups.Translate(typeof(NTAccount)).Select(x=>Role.Create(x.Value));
        }
    }
}