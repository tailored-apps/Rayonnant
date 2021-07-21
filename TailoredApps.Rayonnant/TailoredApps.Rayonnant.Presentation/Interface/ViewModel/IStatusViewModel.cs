using System.Collections.Generic;
using System.Collections.ObjectModel;
using TailoredApps.Rayonnant.Interface.Window;

namespace TailoredApps.Rayonnant.Presentation.Interface.ViewModel
{
    /// <summary>
    ///     Interface for communicating with status view model, if
    /// </summary>
    public interface IStatusViewModel
    {
        /// <summary>
        ///     Contains last message from notification
        /// </summary>
        string Message { get; }

        /// <summary>
        ///     Environment name used to run application , might be a host name or server name for distributed systems.
        /// </summary>
        string Environment { get; }

        /// <summary>
        ///     Current user id.
        /// </summary>
        string UserName { get; }
        ObservableCollection<MenuGroup> MenuGroups { get; set; } 
    }
}