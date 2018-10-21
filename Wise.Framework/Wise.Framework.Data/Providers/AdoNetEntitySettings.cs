using System;
using System.Collections.Generic;
using System.Data;
using Wise.Framework.Data.Enum;
using Wise.Framework.Data.Interface;
using Wise.Framework.Interface.Data;

namespace Wise.Framework.Data.Providers
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