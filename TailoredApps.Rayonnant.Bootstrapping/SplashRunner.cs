using System;
using System.Threading;
using System.Windows.Threading;
using TailoredApps.Rayonnant.Interface.Bootstrapping;
using TailoredApps.Rayonnant.Interface.DependencyInjection;
using TailoredApps.Rayonnant.Interface.Window;
using TailoredApps.Rayonnant.Presentation.Window;

namespace TailoredApps.Rayonnant.Bootstrapping
{
    public class SplashRunner : ISplashRunner
    {
        private readonly IContainer container;
        private readonly ISplashViewModel viewModel;
        private IWindow splash;

        private Thread splashThread;

        public SplashRunner(IContainer container, ISplashViewModel splashViewModel)
        {
            viewModel = splashViewModel;
            this.container = container;
        }

        public void ShowSplash()
        {
            splashThread = new Thread(() =>
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
                {
                    splash = new SplashWindow(viewModel);
                    splash.Show();
                }));
                Dispatcher.Run();
            });
            splashThread.SetApartmentState(ApartmentState.STA);
            splashThread.IsBackground = true;
            splashThread.Start();
        }

        public void CloseSplash()
        {
            Dispatcher.FromThread(splashThread).Invoke(splash.Close);
            viewModel.Dispose();
        }
    }
}