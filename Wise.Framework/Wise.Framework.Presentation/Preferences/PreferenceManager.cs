using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wise.Framework.Presentation.Annotations;
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

        public void SavePreference(string preferenceKey, string value)
        {
            if (string.Equals(preferenceKey, "HomeView"))
            {
                preferenceProvider.HomeView = value.ToString();
            }
            else
            {
                if (!preferenceProvider.Preferences.ContainsKey(preferenceKey))
                {
                    preferenceProvider.Preferences.Add(preferenceKey, value);
                }
                else
                {
                    preferenceProvider.Preferences[preferenceKey] = value;
                }

            }
            preferenceProvider.Save();
        }

        public string GetPreference(string preferenceKey)
        {
            return preferenceProvider.GetPreferenceByKey(preferenceKey);
        }


        
    }

    
}
