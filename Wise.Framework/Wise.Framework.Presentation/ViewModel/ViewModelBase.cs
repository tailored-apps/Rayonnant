using System;
using System.Linq;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Regions;
using Wise.Framework.Presentation.Commands;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Wise.Framework.Presentation.ViewModel
{
    /// <summary>
    ///     Default ViewModelBase class
    /// </summary>
    public abstract class ViewModelBase :  IDisposable, INavigationAware, INotifyPropertyChanged
    {
        private string screenId;

 
        public string ScreenId
        {
            get { return screenId; }
            protected set { SetProperty(ref screenId, value); }
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
            ScreenId = Guid.NewGuid().ToString();
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
            string id = navigationContext.Parameters.Any(x => "ScreenId".Equals(x.Key)) ? navigationContext.Parameters["ScreenId"].ToString() : string.Empty;
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
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <typeparam name="T">The type of the property that has a new value</typeparam>
        /// <param name="propertyExpression">A Lambda expression representing the property that has a new value.</param>
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            this.OnPropertyChanged(propertyName);
        }
    }
}