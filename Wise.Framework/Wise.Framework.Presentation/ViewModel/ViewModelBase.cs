using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.ViewModel;
using System;

namespace Wise.Framework.Presentation.ViewModel
{
    /// <summary>
    /// Default ViewModelBase class  
    /// </summary>
    public abstract class ViewModelBase : NotificationObject, IDisposable
    {
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
    }
}
