using System;
using System.Windows;
using Common.Logging;
using Wise.Framework.Presentation;
using Wise.Framework.Bootstrapping;

namespace Wise.HostShell
{

    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : WiseApplication
    {
        private readonly ILog log = LogManager.GetCurrentClassLogger();

       
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new BootsraperImpl(log);
            new BootstrapperRunner().Run(bootstrapper);
        }

        protected override void LogUnhandledException(Exception exception)
        {
            log.Error(exception);
        }
    }
}
