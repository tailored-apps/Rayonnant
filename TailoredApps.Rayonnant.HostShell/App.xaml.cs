using System;
using System.Windows;
using Common.Logging;
using TailoredApps.Rayonnant.Bootstrapping;
using TailoredApps.Rayonnant.Presentation;

namespace TailoredApps.Rayonnant.HostShell
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : WiseApplication
    {
        private readonly ILog log = LogManager.GetLogger< App>();

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