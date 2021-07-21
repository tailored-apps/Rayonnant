using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailoredApps.Rayonnant.Data.Enum
{
    [Flags]
    public enum AdoNetProviderOperations
    {
        Get = 0,
        GetAll = 1,
        GetById = 2,
        Save = 4,
        Delete = 8,
        DeleteById = 16,
        GetBySearchCriteria = 32

    }
}
