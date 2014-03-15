using System.Collections.ObjectModel;

namespace Wise.Framework.Presentation.Interface.ViewModel
{
    /// <summary>
    /// Interface used to present command in tool bars.
    /// </summary>
    public interface ICommandsViewModel
    {
        /// <summary>
        /// Commands
        /// </summary>
        ObservableCollection<ICommandsViewModel> Commands { get; set; }

        /// <summary>
        /// Tels information about contained commands in application
        /// </summary>
        bool HasCommands { get; }
    }
}
