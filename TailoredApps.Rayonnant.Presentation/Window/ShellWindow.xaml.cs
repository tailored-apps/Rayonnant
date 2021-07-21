using System;
using System.Linq;
using TailoredApps.Rayonnant.Interface.Window;
using TailoredApps.Rayonnant.Presentation.Interface.Shell;
using TailoredApps.Rayonnant.Presentation.Interface.ViewModel;
using TailoredApps.Rayonnant.Presentation.ViewModel;

namespace TailoredApps.Rayonnant.Presentation.Window
{
    /// <summary>
    ///     Interaction logic for Window1.xaml
    /// </summary>
    public partial class ShellWindow : WindowBase, IShellWindow
    {
        /// <summary>
        ///     Ctor for shell window object
        /// </summary>
        /// <param name="shellViewModel">default shell window view model</param>
        /// c
        public ShellWindow(IShellViewModel shellViewModel)
        {
            DataContext = shellViewModel;

            InitializeComponent();
        }

    }
}