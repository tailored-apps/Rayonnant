using System;
using Wise.Framework.Presentation.Interface.ViewModel;

namespace Wise.Framework.Presentation.ViewModel
{
    /// <summary>
    /// <see cref="IShellViewModel"/>
    /// </summary>
    public class ShellViewModel : ViewModelBase , IShellViewModel
    {
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
    }
}
