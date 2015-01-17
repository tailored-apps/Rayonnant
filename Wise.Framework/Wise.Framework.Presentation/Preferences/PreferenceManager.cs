using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Logging;

namespace Wise.Framework.Presentation.Preferences
{
    public class PreferenceManager : IPreferenceManager
    {
        private readonly IPreferenceProvider preferenceProvider;
        public PreferenceManager(IPreferenceProvider preferenceProvider)
        {
            this.preferenceProvider = preferenceProvider;
        }

        public string GetUserHomeView()
        {
            return preferenceProvider.HomeView;
        }
    }

    
}
