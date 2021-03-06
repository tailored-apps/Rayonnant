using System;

namespace TailoredApps.Rayonnant.Presentation.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MenuItemAttribute : Attribute
    {
       
        public string Path { get;  set; }
        public string DisplayName { get;  set; }
        public string NavigationParameters { get;  set; }
    }
}