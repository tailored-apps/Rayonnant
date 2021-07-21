using System;
using System.Collections.Generic;
using System.Data;
using TailoredApps.Rayonnant.Data.Enum;
using TailoredApps.Rayonnant.Data.Interface;
using TailoredApps.Rayonnant.Interface.Data;

namespace TailoredApps.Rayonnant.Data.Providers
{
    public class AdoNetEntitySettings
    {
        public AdoNetEntitySettings()
        {
            Operations = new List<AdoNetProviderOperations>();
        }

        public object Execution { get; internal set; }
        public IList<AdoNetProviderOperations> Operations { get;  set; }
    }
}