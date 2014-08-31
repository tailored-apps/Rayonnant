using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.InternalApplicationMessagning.Enum;
using Wise.Framework.Interface.Window;
using Wise.Framework.InternalMessagning;

namespace Wise.Framework.Presentation.ViewModel
{
    public class SplashViewModel : INotifyPropertyChanged, ISplashViewModel, IDisposable
    {
        private IDisposable messangerSubscription;
        public SplashViewModel(IMessanger messanger)
        {
            Messages = new ObservableCollection<string>();
            messangerSubscription = messanger.Subscribe<SystemNotyficationMessage>(OnMessageArrive).ExecuteOn(MessageProcessingThread.Dispatcher);
        }

        private void OnMessageArrive(SystemNotyficationMessage obj)
        {
            if (SplashDispatcher != null && Messages != null)
            {
                SplashDispatcher.Invoke(new Action(() =>
                {
                    Messages.Insert(0, obj.Message);
                    CurrentMessage = obj.Message;
                }));
            }
        }

        public ObservableCollection<string> Messages { get; set; }

        private string currentMessage;
        public string CurrentMessage
        {
            get { return currentMessage; }
            set
            {
                currentMessage = value;
                OnPropertyChanged("CurrentMessage");
            }
        }

        private string applicationName;
        public string ApplicationName
        {
            get { return applicationName; }
            set
            {
                applicationName = value;
                OnPropertyChanged("ApplicationName");
            }
        }

        private string enviormentName;
        public string EnviormentName
        {
            get { return enviormentName; }
            set
            {
                enviormentName = value;
                OnPropertyChanged("EnviormentName");
            }
        }

        private string productName;
        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }

        private string version;
        public string Version
        {
            get { return version; }
            set
            {
                version = value;
                OnPropertyChanged("Version");
            }
        }

        public Uri Logo { get; set; }
        public Dispatcher SplashDispatcher { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public void Dispose()
        {
            messangerSubscription.Dispose();
        }
    }
}
