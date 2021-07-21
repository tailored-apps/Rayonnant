using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailoredApps.Rayonnant.Presentation.Annotations;
using TailoredApps.Rayonnant.Presentation.Interface;
using TailoredApps.Rayonnant.Presentation.Logging;

namespace TailoredApps.Rayonnant.Presentation.Preferences
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
