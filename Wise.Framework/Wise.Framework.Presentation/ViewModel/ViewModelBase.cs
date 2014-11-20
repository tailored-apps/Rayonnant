using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using Wise.Framework.Presentation.Commands;

namespace Wise.Framework.Presentation.ViewModel
{
    /// <summary>
    ///     Default ViewModelBase class
    /// </summary>
    public abstract class ViewModelBase : BindableBase, IDisposable, INavigationAware
    {
        private string screenId;

 
        public string ScreenId
        {
            get { return screenId; }
            private set { SetProperty(ref screenId, value); }
        }

        private bool isTearOff;
        public bool IsTearOff
        {
            get { return isTearOff; }
            set { SetProperty(ref isTearOff, value); }
        }


        public ViewModelBase()
        {
            CloseItemCommand = new CloseItemCommand();
        }


        /// <summary>
        ///     <see cref="IDisposable.Dispose" />
        /// </summary>
        public void Dispose()
        {
            OnDispose();
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            object id = navigationContext.Parameters["ID"];
            return Equals(ScreenId, id);
        }


        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }


        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            ScreenId = navigationContext.Parameters.Any(x => "ScreenId".Equals(x.Key)) ? navigationContext.Parameters["ScreenId"].ToString() : string.Empty;
        }

        /// <summary>
        ///     Method which will be run on class disposing.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

        private ICommand closeItemCommand;
        public ICommand CloseItemCommand
        {
            get { return closeItemCommand; }
            private set
            {
                SetProperty(ref closeItemCommand, value);
            }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private Uri icon;

        public Uri Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }

    }
}