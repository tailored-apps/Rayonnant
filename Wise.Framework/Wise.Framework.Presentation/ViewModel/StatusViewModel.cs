using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Wise.Framework.Interface.Environment;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.InternalApplicationMessagning.Enum;
using Wise.Framework.Interface.Security;
using Wise.Framework.Interface.Window;
using Wise.Framework.InternalMessagning;
using Wise.Framework.Presentation.Interface.ViewModel;

namespace Wise.Framework.Presentation.ViewModel
{
    /// <summary>
    ///     <see cref="IStatusViewModel" />
    /// </summary>
    public class StatusViewModel : ViewModelBase, IStatusViewModel
    {
        private readonly IDisposable messageSubscription;
        private string environment;
        private IEnvironmentService environmentService;

        private string message;
        private IMessanger messanger;
        private ISecurityService securityService;
        private string userName;

        public StatusViewModel(ISecurityService securityService, IEnvironmentService environmentService,
            IMessanger messanger)
        {
            this.messanger = messanger;
            messageSubscription =
                messanger.Subscribe<SystemNotyficationMessage>(OnMessageArrive)
                    .ExecuteOn(MessageProcessingThread.Dispatcher);

            this.securityService = securityService;
            this.environmentService = environmentService;
            UserName = securityService.User.Name;
            Environment = environmentService.GetEnvironmentInfo().Code;
        }

        /// <summary>
        ///     Last status message
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
        ///     Environment name, also might be a machine name
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
        ///     User Name id
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private ObservableCollection<MenuGroup> menuGroups;
        public ObservableCollection<MenuGroup> MenuGroups
        {
            get { return menuGroups; }
            set
            {
                menuGroups = value;
                OnPropertyChanged("ViewModels");
            }
            
        }

        private void OnMessageArrive(SystemNotyficationMessage obj)
        {
            Message = obj.Message;
        }

        protected override void OnDispose()
        {
            messageSubscription.Dispose();
            base.OnDispose();
        }
    }
}