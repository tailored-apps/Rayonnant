using System;
using System.Windows.Input;
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
        public string ScreenId { get; protected set; }
        public bool IsTearOff { get; set; }

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
    }
}