using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace TailoredApps.Rayonnant.Interface.Window
{
    public interface ISplashViewModel : IDisposable
    {
        /// <summary>
        ///     Messages
        /// </summary>
        ObservableCollection<string> Messages { get; set; }

        /// <summary>
        ///     Last Message
        /// </summary>
        string CurrentMessage { set; get; }

        /// <summary>
        ///     Application name
        /// </summary>
        string ApplicationName { get; set; }

        /// <summary>
        ///     Environment  name
        /// </summary>
        string EnviormentName { get; set; }

        /// <summary>
        ///     Product name
        /// </summary>
        string ProductName { get; set; }

        /// <summary>
        ///     Application version no
        /// </summary>
        string Version { get; set; }

        /// <summary>
        ///     Dispatcher for splash screen
        /// </summary>
        Dispatcher SplashDispatcher { get; set; }

        Uri Logo { get; set; }
    }
}