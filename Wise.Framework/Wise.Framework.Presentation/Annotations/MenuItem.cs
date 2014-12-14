using System;
using Microsoft.Practices.Prism.Regions;

namespace Wise.Framework.Presentation.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MenuItem : Attribute
    {
        public MenuItem(string path, string displayName, string navigationParameters)
        {
            DisplayName = displayName;
            Path = path;
            NavigationParameters = navigationParameters;
        }
        public MenuItem(string path, string displayName)
            : this(path, displayName, null)
        {
        }

        public string Path { get; set; }
        public string DisplayName { get; set; }
        public string NavigationParameters { get; set; }
    }
}