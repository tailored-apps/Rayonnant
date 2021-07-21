using TailoredApps.Rayonnant.Interface.Window;

namespace TailoredApps.Rayonnant.Presentation.Window
{
    /// <summary>
    ///     Interaction logic for Window1.xaml
    /// </summary>
    public partial class SplashWindow : WindowBase
    {
        /// <summary>
        ///     Ctor for shell window object
        /// </summary>
        /// <param name="splashViewModel">default shell window view model</param>
        /// c
        public SplashWindow(ISplashViewModel splashViewModel)
        {
            splashViewModel.SplashDispatcher = Dispatcher;
            DataContext = splashViewModel;
            InitializeComponent();
        }
    }
}