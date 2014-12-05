using System.Collections.Generic;

namespace Wise.Framework.Security.Interface
{
    public interface IActiveDirectory
    {
        IList<string> GetUserNames();
    }
}
