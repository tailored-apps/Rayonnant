using System;
using System.Collections.ObjectModel;
using TailoredApps.Rayonnant.Presentation.ViewModel;

namespace TailoredApps.Rayonnant.Presentation.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ViewModelInfoAttribute : Attribute
    {
        private string roles ;
     
        public ViewModelBase ViewModel { get; set; }
        public Type ViewModelType { get; set; }
        public string DisplayName { get;  set; }
        public int SecurityLevel { get;  set; }
        public string MenuGroup { get;  set; }
        public string NavigationParameters { get;  set; }

        public string AllowedRoles
        {
            get { return roles; }
            set { roles = value; }
        }

        public override bool Equals(object obj)
        {
            var oth = obj as ViewModelInfoAttribute;
            if(oth!=null)
            return oth.ViewModelType.Equals(this.ViewModelType);

            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}