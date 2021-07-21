using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TailoredApps.Rayonnant.Interface.Window;

namespace TailoredApps.Rayonnant.Presentation.TemplateSelectors
{
    public class StatusMenuGroupTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var menuGroup = item as MenuGroup;
            var template=Application.Current.FindResource(menuGroup.DataTemplateKey);
            return template as DataTemplate;
        }
    }
}
