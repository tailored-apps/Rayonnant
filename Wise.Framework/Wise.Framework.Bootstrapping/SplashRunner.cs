using System;
using System.Threading;
using System.Windows.Threading;
using Wise.Framework.Interface.Bootstrapping;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.Window;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Window;

namespace Wise.Framework.Bootstrapping
{
    public class SplashRunner : ISplashRunner
    {
        private readonly IContainer container;
        private IWindow splash;
        private readonly ISplashViewModel viewModel;
        public SplashRunner(IContainer container,ISplashViewModel splashViewModel )
        {
            viewModel = splashViewModel;
            this.container = container;
        }

        private Thread splashThread;
        public void ShowSplash()
        {
            splashThread = new Thread(() =>
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
                {

                    this.splash = new SplashWindow(viewModel);
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
            splashThread.Abort();
            viewModel.Dispose();
        }
    }
}
