using System;
using System.Collections.ObjectModel;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.Framework.Presentation.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ViewModelInfoAttribute : Attribute
    {
        public ViewModelInfoAttribute(string displayName, string menuGroup, int securityLevel, string navigationParameters)
        {
            DisplayName = displayName;
            MenuGroup = menuGroup;
            SecurityLevel = securityLevel;
            NavigationParameters = navigationParameters;
        }

        public ViewModelInfoAttribute(string displayName, string menuGroup, string navigationParameters)
            : this(displayName, menuGroup, 0, navigationParameters)
        {
        }
        public ViewModelInfoAttribute(string displayName, string menuGroup, int securityLevel)
            : this(displayName, menuGroup, securityLevel, null)
        {
        }

        public ViewModelInfoAttribute(string displayName, string menuGroup)
            : this(displayName, menuGroup, 0)
        {
        }

        public ViewModelInfoAttribute(string displayName)
            : this(displayName, string.Empty)
        {
        }

        public ViewModelBase ViewModel { get; set; }
        public Type ViewModelType { get; set; }
        public string DisplayName { get; set; }
        public int SecurityLevel { get; set; }
        public string MenuGroup { get; set; }
        public string NavigationParameters { get; set; }

        public override bool Equals(object obj)
        {
            var oth = obj as ViewModelInfoAttribute;
            if(oth!=null)
            return oth.ViewModelType.Equals(this.ViewModelType);

            return base.Equals(obj);
        }
    }
}