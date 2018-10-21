using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Wise.Framework.Presentation.Window;

namespace Wise.Framework.Presentation.Behaviors
{
    public class KeyUpBehavior : Behavior<WindowBase>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.KeyUp += AssociatedObject_KeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.KeyUp -= AssociatedObject_KeyDown;
        }

        void AssociatedObject_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.HasValue)
            {
                if (e.SystemKey != Key)
                {
                    return;
                }
            }
            if (KeyDownCommand != null && KeyDownCommand.CanExecute(e.Key))
            {
                KeyDownCommand.Execute(e.Key);
            }
        }

        public ICommand KeyDownCommand
        {
            get { return (ICommand)GetValue(KeyDownCommandProperty); }
            set { SetValue(KeyDownCommandProperty, value); }
        }

        public Key? Key
        {
            get { return (Key?) GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }


        // Using a DependencyProperty as the backing store for ShouldHaveFocus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyDownCommandProperty =
            DependencyProperty.Register("KeyDownCommand", typeof(ICommand), typeof(KeyUpBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof (Key?), typeof (KeyUpBehavior), new PropertyMetadata(default(Key?)));
    }
}
