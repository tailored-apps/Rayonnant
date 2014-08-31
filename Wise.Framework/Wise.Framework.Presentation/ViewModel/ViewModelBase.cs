using System;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;

namespace Wise.Framework.Presentation.ViewModel
{
    /// <summary>
    /// Default ViewModelBase class 
    /// </summary>
    public abstract class ViewModelBase : BindableBase , IDisposable, INavigationAware
    {
        public string ScreenId { get;  protected set; }

        /// <summary>
        /// <see cref="IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// Method which will be run on class disposing.
        /// </summary>
        protected virtual void OnDispose()
        {

        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var id = navigationContext.Parameters["ID"];
            return string.Equals(ScreenId, id);
        }


        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }


        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

    }
}
