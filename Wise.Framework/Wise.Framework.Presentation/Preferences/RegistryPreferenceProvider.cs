using System.Collections.Generic;

namespace Wise.Framework.Presentation.Preferences
{
    public class RegistryPreferenceProvider : IPreferenceProvider
    {
        private const string HOME_VIEW_KEY = "HomeView";

        public RegistryPreferenceProvider()
        {
            if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(GetStoreKey()) == null)
            {
                Microsoft.Win32.Registry.CurrentUser.CreateSubKey(GetStoreKey());
            }
            HomeView = ReadValue(HOME_VIEW_KEY);
            Preferences = ReadPreferences();
        }

        private Dictionary<string, string> ReadPreferences()
        {
            var dict = new Dictionary<string, string>();
            var valueNames = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(GetStoreKey()).GetValueNames();
            foreach (var valueName in valueNames)
            {
                if (!string.Equals(valueName, HOME_VIEW_KEY))
                    dict.Add(valueName, Microsoft.Win32.Registry.CurrentUser.OpenSubKey(GetStoreKey()).GetValue(valueName).ToString());
            }
            return dict;
        }

        private string homeView;
        public string HomeView
        {
            get { return homeView; }
            set { homeView = value; }
        }

        public Dictionary<string, string> Preferences { get; set; }

        public void Save()
        {
            SaveValue(HOME_VIEW_KEY, HomeView);

            foreach (var preference in Preferences)
            {
                SaveValue(preference.Key, preference.Value);
            }
        }


        public string GetPreferenceByKey(string preferenceKey)
        {
            return ReadValue(preferenceKey);
        }

        private string ReadValue(string key)
        {
            var registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(GetStoreKey()).GetValue(key);
            return registryKey != null ? registryKey.ToString() : null;
        }

        private void SaveValue(string key, string value)
        {
            var regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(GetStoreKey(), true);
            if (value != null)
            {
                regkey.SetValue(key, value ?? string.Empty);

            }
        }

        private string GetStoreKey()
        {
            return string.Format("Software\\Wise\\{0}\\v{1}", WiseApplication.Name, WiseApplication.Version);
        }


    }
}
