using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailoredApps.Rayonnant.Presentation.Preferences
{
    public interface IPreferenceProvider
    {
        string HomeView { get; set; }
        Dictionary<string,string> Preferences { get; set; }

        void Save();
        string GetPreferenceByKey(string preferenceKey);
    }
}
