using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TailoredApps.Rayonnant.Interface.Environment;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning.Enum;
using TailoredApps.Rayonnant.Interface.Security;
using TailoredApps.Rayonnant.Interface.Window;
using TailoredApps.Rayonnant.InternalMessagning;
using TailoredApps.Rayonnant.Presentation.Interface.ViewModel;

namespace TailoredApps.Rayonnant.Presentation.ViewModel
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