using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TailoredApps.Rayonnant.Interface.Security;
using TailoredApps.Rayonnant.Security.Interface;

namespace TailoredApps.Rayonnant.Security.Providers
{
    public class ActiveDirectoryProvider : BaseSecurityProvider, IActiveDirectory
    {
        public override IIdentity GetCurrentIddentity()
        {
            return WindowsIdentity.GetCurrent();
        }

        public override bool IsInRole(string roleName)
        {
            var roles = GetRoles(GetCurrentIddentity());
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    if (string.Equals(roleName, role.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override IEnumerable<IRole> GetRoles(IIdentity user)
        {
            var ctx = new PrincipalContext(ContextType.Domain);

            // find a user
            var userPrincipal = UserPrincipal.FindByIdentity(ctx, user.Name);

            if (userPrincipal != null)
            {
                PrincipalSearchResult<Principal> authgroups = userPrincipal.GetAuthorizationGroups();
                foreach (var authgroup in authgroups)
                {
                    yield return Role.Create(authgroup.Name);
                }
            }
        }

        public IList<string> GetUserNames()
        {
            var dominio = new DirectoryEntry("LDAP://rule", "lkowalski", "wrzesien13!!");

            DirectorySearcher ds = new DirectorySearcher("(objectclass=user)");
            ds.SearchRoot = dominio;
            ds.SearchScope = SearchScope.Subtree;

            SearchResultCollection res = ds.FindAll();

            IList<string> uski = new List<string>(res.Count);

            foreach (SearchResult r in res)
            {
                DirectoryEntry dentry = r.GetDirectoryEntry();
                uski.Add(dentry.Properties["name"].Value.ToString());
            }
            return null;
        }
    }
}
