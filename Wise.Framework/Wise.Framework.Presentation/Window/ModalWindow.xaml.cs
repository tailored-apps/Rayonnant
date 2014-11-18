using System.Windows;
using System.Windows.Input;
using Wise.Framework.Interface.Window;
using Wise.Framework.Presentation.Commands;

namespace Wise.Framework.Presentation.Window
{
    public partial class ModalWindow : WindowBase, IModalWindow
    {
        public ModalWindow()
        {
            InitializeComponent();
        }


        public void Dock()
        {
            if (DockCommand != null && DockCommand.CanExecute(DataContext))
            {
                DockCommand.Execute(DataContext);
            }
        }

        public static readonly DependencyProperty DockCommandProperty = DependencyProperty.Register(
            "DockCommand", typeof (ICommand), typeof (ModalWindow), new PropertyMetadata(new DockCommand()));

        public ICommand DockCommand
        {
            get { return (ICommand) GetValue(DockCommandProperty); }
            set { SetValue(DockCommandProperty, value); }
        }
    }
}