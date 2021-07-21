using System.Collections.ObjectModel;
using System.Windows.Controls;
using TailoredApps.Rayonnant.Presentation.Interface.ViewModel;

namespace TailoredApps.Rayonnant.Presentation.ViewModel
{
    /// <summary>
    ///     Default implementation of <see cref="ICommandsViewModel" />
    /// </summary>
    public class CommandsViewModel : ViewModelBase, ICommandsViewModel
    {
        private ObservableCollection<MenuItem> commands;

        public CommandsViewModel()
        {
            Commands = new ObservableCollection<MenuItem>();
        }

        /// <summary>
        ///     Command list <see cref="ICommandsViewModel.Commands" />
        /// </summary>
        public ObservableCollection<MenuItem> Commands
        {
            get { return commands; }
            set
            {
                commands = value;
                OnPropertyChanged("Commands");
            }
        }
    }
}