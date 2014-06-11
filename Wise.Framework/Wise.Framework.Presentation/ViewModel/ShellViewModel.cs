using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Wise.Framework.Presentation.Interface.ViewModel;

namespace Wise.Framework.Presentation.ViewModel
{
    /// <summary>
    /// <see cref="IShellViewModel"/>
    /// </summary>
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {

        public ShellViewModel()
        {
            IsVisibleCommandRegion =true;
            ToogleVisibilityCommandRegionCommand = new DelegateCommand(toogleVisibilityOnCommandRegion);
        }

        private Uri icon;
        private string title;
        /// <summary>
        /// <see cref="IShellViewModel.Title"/>
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged("Title");

            }
        }

        /// <summary>
        /// <see cref="IShellViewModel.Icon"/>
        /// </summary>
        public Uri Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                RaisePropertyChanged("Icon");

            }
        }

        private bool isVisibleCommandRegion;
        public bool IsVisibleCommandRegion
        {
            get { return isVisibleCommandRegion; }
            set
            {
                isVisibleCommandRegion = value;
                RaisePropertyChanged("IsVisibleCommandRegion");
            }
        }

        private ICommand toogleVisibilityCommandRegionCommand;
        public ICommand ToogleVisibilityCommandRegionCommand
        {
            get { return toogleVisibilityCommandRegionCommand; }
            set
            {
                toogleVisibilityCommandRegionCommand = value;
                RaisePropertyChanged("ToogleVisibilityCommandRegionCommand");
            }
        }


        private void toogleVisibilityOnCommandRegion()
        {
            IsVisibleCommandRegion = !IsVisibleCommandRegion;
        } 
    }
}
