using System.Collections.ObjectModel;
using Wise.Framework.Presentation.Interface.ViewModel;

namespace Wise.Framework.Presentation.ViewModel
{
    /// <summary>
    /// Default implementation of <see cref="ICommandsViewModel"/>
    /// </summary>
    public class CommandsViewModel : ICommandsViewModel
    {
        /// <summary>
        /// Command list <see cref="ICommandsViewModel.Commands"/>
        /// </summary>
        public ObservableCollection<ICommandsViewModel> Commands
        {
            get; set;
        }

        /// <summary>
        /// checks is Commands not null and contains some operations ;D
        /// <see cref="ICommandsViewModel.HasCommands"/>
        /// </summary>
        public bool HasCommands
        {
            get { return Commands != null && Commands.Count > 0; }
        }
    }
}
