using System.Security.Principal;

namespace Wise.Framework.Interface.Security
{
    public interface ISecurityService
    {
        IIdentity User { get; }
    }
}
