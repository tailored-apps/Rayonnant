namespace Wise.Framework.Presentation.Interface
{
    public interface IPreferenceManager
    {
        string GetUserHomeView();


        void SavePreference(string preferenceKey, object value);
    }
}
