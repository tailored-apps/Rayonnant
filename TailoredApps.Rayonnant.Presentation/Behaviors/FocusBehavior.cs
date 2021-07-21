using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace TailoredApps.Rayonnant.Presentation.Behaviors
{
    public class FocusBehavior : Behavior<DependencyObject>
    {

        public bool ShouldHaveFocus
        {
            get { return (bool)GetValue(ShouldHaveFocusProperty); }
            set { SetValue(ShouldHaveFocusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShouldHaveFocus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShouldHaveFocusProperty =
            DependencyProperty.Register("ShouldHaveFocus", typeof(bool), typeof(FocusBehavior), new PropertyMetadata(false));

    }
}
