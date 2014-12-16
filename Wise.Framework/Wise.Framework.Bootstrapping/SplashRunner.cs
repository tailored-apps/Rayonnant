using System;
using System.Threading;
using System.Windows.Threading;
using Wise.Framework.Interface.Bootstrapping;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.Window;
using Wise.Framework.Presentation.Window;

namespace Wise.Framework.Bootstrapping
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
            //this need to be checked
            Dispatcher.FromThread(splashThread).Invoke(splash.Close);
            viewModel.Dispose();
        }
    }
}