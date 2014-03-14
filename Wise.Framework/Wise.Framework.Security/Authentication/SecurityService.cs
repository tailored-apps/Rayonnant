using System.Security.Principal;
using Wise.Framework.Interface.Security;

namespace Wise.Framework.Security.Authentication
{
    public class SecurityService : ISecurityService
    {

        public IIdentity User { get { return WindowsIdentity.GetCurrent(); } }
    }
}
