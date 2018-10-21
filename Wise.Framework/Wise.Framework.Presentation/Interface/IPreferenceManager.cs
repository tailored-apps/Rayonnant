using System;

namespace Wise.Framework.Presentation.Interface
{
    public interface IPreferenceManager
    {
        string GetUserHomeView();


        void SavePreference(string preferenceKey, string value);
        string GetPreference(string preferenceKey);


    }
}
