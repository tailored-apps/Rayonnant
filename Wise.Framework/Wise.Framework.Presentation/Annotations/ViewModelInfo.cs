using System;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Regions;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.Framework.Presentation.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ViewModelInfo : Attribute
    {
        public ViewModelInfo(string displayName, string menuGroup, int securityLevel, string navigationParameters)
        {
            DisplayName = displayName;
            MenuGroup = menuGroup;
            SecurityLevel = securityLevel;
            NavigationParameters = navigationParameters;
        }

        public ViewModelInfo(string displayName, string menuGroup, string navigationParameters)
            : this(displayName, menuGroup, 0, navigationParameters)
        {
        }
        public ViewModelInfo(string displayName, string menuGroup, int securityLevel)
            : this(displayName, menuGroup, securityLevel, null)
        {
        }

        public ViewModelInfo(string displayName, string menuGroup)
            : this(displayName, menuGroup, 0)
        {
        }

        public ViewModelInfo(string displayName)
            : this(displayName, string.Empty)
        {
        }

        public ViewModelBase ViewModel { get; set; }
        public Type ViewModelType { get; set; }
        public string DisplayName { get; set; }
        public int SecurityLevel { get; set; }
        public string MenuGroup { get; set; }
        public string NavigationParameters { get; set; }
    }
}