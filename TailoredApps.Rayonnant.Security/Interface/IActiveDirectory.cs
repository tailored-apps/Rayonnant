using System.Collections.Generic;

namespace TailoredApps.Rayonnant.Security.Interface
{
    public interface IActiveDirectory
    {
        IList<string> GetUserNames();
    }
}
