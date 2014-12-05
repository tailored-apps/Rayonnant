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
        private readonly IDisposable messangerSubscription;
        private string applicationName;
        private string currentMessage;
        private string enviormentName;
        private string productName;
        private string version;

        public SplashViewModel(IMessanger messanger)
        {
            Messages = new ObservableCollection<string>();
            messangerSubscription =
                messanger.Subscribe<SystemNotyficationMessage>(OnMessageArrive)
                    .ExecuteOn(MessageProcessingThread.Dispatcher);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Messages { get; set; }

        public string CurrentMessage
        {
            get { return currentMessage; }
            set
            {
                currentMessage = value;
                OnPropertyChanged("CurrentMessage");
            }
        }

        public string ApplicationName
        {
            get { return applicationName; }
            set
            {
                applicationName = value;
                OnPropertyChanged("ApplicationName");
            }
        }

        public string EnviormentName
        {
            get { return enviormentName; }
            set
            {
                enviormentName = value;
                OnPropertyChanged("EnviormentName");
            }
        }

        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }

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


        public void Dispose()
        {
            messangerSubscription.Dispose();
        }

        private void OnMessageArrive(SystemNotyficationMessage obj)
        {
            if (SplashDispatcher != null && Messages != null)
            {
                SplashDispatcher.Invoke(() =>
                {
                    Messages.Insert(0, obj.Message);
                    CurrentMessage = obj.Message;
                });
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}