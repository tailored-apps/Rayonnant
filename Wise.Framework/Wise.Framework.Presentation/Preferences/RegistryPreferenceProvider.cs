using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        private string homeView;
        public string HomeView
        {
            get { return homeView; }
            set { homeView = value; }
        }

        public void Save()
        {
            SaveValue(HOME_VIEW_KEY, HomeView);
        }

        private string ReadValue(string key)
        {
            var registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(GetStoreKey()).GetValue(key);
            return registryKey != null ? registryKey.ToString() : null;
        }
        private void SaveValue(string key, string value)
        {
            Microsoft.Win32.Registry.CurrentUser.OpenSubKey(GetStoreKey(),true).SetValue(key, value);
        }

        private string GetStoreKey()
        {
            return string.Format("Software\\Wise\\{0}\\v{1}", WiseApplication.Name, WiseApplication.Version);
        }


    }
}
