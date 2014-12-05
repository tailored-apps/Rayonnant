using System;
using System.Windows;
using Common.Logging;
using Wise.Framework.Bootstrapping;
using Wise.Framework.Presentation;

namespace Wise.Framework.HostShell
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