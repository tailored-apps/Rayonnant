using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;

namespace Wise.Framework.Interface.Security
{
    public interface ISecurityService
    {
        IIdentity User { get; }

        bool IsInRole(string roleName);

        IEnumerable<IRole> GetUserRoles(IIdentity user);
    }
}