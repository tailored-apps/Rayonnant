using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Wise.Framework.Presentation.Interface.ViewModel;

namespace Wise.Framework.Presentation.ViewModel
{
    /// <summary>
    ///     <see cref="IShellViewModel" />
    /// </summary>
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        private Uri icon;

        private bool isVisibleCommandRegion;
        private string title;

        private ICommand toogleVisibilityCommandRegionCommand;

        public ShellViewModel()
        {
            IsVisibleCommandRegion = true;
            ToogleVisibilityCommandRegionCommand = new DelegateCommand(toogleVisibilityOnCommandRegion);
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

        /// <summary>
        ///     <see cref="IShellViewModel.Title" />
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        ///     <see cref="IShellViewModel.Icon" />
        /// </summary>
        public Uri Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged("Icon");
            }
        }


        private void toogleVisibilityOnCommandRegion()
        {
            IsVisibleCommandRegion = !IsVisibleCommandRegion;
        }
    }
}