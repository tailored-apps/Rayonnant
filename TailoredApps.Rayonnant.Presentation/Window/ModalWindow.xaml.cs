using System.Windows;
using System.Windows.Input;
using TailoredApps.Rayonnant.Interface.Window;
using TailoredApps.Rayonnant.Presentation.Commands;

namespace TailoredApps.Rayonnant.Presentation.Window
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