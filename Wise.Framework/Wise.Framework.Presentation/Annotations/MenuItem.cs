using System;

namespace Wise.Framework.Presentation.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MenuItem : Attribute
    {
        public MenuItem(string path, string displayName)
        {
            DisplayName = displayName;
            Path = path;
        }

        public string Path { get; set; }
        public string DisplayName { get; set; }
    }
}