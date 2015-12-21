using System.Windows.Input;
using Prism.Commands;
using Wise.Framework.Presentation.Interface.ViewModel;

namespace Wise.Framework.Presentation.ViewModel
{
    /// <summary>
    ///     <see cref="IShellViewModel" />
    /// </summary>
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {

        private bool isVisibleCommandRegion;

        private ICommand toogleVisibilityCommandRegionCommand;

        public ShellViewModel()
        {
            IsVisibleCommandRegion = false;
            ToogleVisibilityCommandRegionCommand = new DelegateCommand(ToogleVisibilityOnCommandRegion);
        }

        public bool IsVisibleCommandRegion
        {
            get { return isVisibleCommandRegion; }
            set
            {
                isVisibleCommandRegion = value;
                OnPropertyChanged("IsVisibleCommandRegion");
            }
        }

        public ICommand ToogleVisibilityCommandRegionCommand
        {
            get { return toogleVisibilityCommandRegionCommand; }
            set
            {
                toogleVisibilityCommandRegionCommand = value;
                OnPropertyChanged("ToogleVisibilityCommandRegionCommand");
            }
        }

     


        private void ToogleVisibilityOnCommandRegion()
        {
            IsVisibleCommandRegion = !IsVisibleCommandRegion;
        }
    }
}