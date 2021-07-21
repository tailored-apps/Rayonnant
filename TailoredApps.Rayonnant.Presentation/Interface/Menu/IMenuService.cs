using System.Windows.Controls;

namespace TailoredApps.Rayonnant.Presentation.Interface.Menu
{
    public interface IMenuService
    {
        MenuItem GetMenuItem(string path);
        void AddMenuItem(MenuItem menuItem, string path);
        void RemoveMenuItem(string path);
    }
}