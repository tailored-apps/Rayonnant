using System;
using Wise.Framework.Interface.Environment;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.InternalApplicationMessagning.Enum;
using Wise.Framework.Interface.Security;
using Wise.Framework.InternalMessagning;
using Wise.Framework.Presentation.Interface.ViewModel;

namespace Wise.Framework.Presentation.ViewModel
{
    /// <summary>
    /// <see cref="IStatusViewModel"/>
    /// </summary>
    public class StatusViewModel : ViewModelBase, IStatusViewModel
    {
        private ISecurityService securityService;
        private IEnvironmentService environmentService;
        private IMessanger messanger;
        private IDisposable messageSubscription;
        public StatusViewModel(ISecurityService securityService, IEnvironmentService environmentService, IMessanger messanger)
        {
            this.messanger = messanger;
            messageSubscription = messanger.Subscribe<SystemNotyficationMessage>(OnMessageArrive).ExecuteOn(MessageProcessingThread.Dispatcher);

            this.securityService = securityService;
            this.environmentService = environmentService;
            UserName = securityService.User.Name;
            Environment = environmentService.GetEnvironmentInfo().Code;
        }

        private void OnMessageArrive(SystemNotyficationMessage obj)
        {
            Message = obj.Message;
        }

        private string message;
        private string userName;
        private string environment;

        /// <summary>
        /// Last status message 
        /// </summary>
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        /// <summary>
        /// Environment name, also might be a machine name
        /// </summary>
        public string Environment
        {
            get { return environment; }
            protected set
            {
                environment = value;
                OnPropertyChanged("Environment");
            }
        }


        /// <summary>
        /// User Name id
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        protected override void OnDispose()
        {
            messageSubscription.Dispose();
            base.OnDispose();
        }
    }
}
