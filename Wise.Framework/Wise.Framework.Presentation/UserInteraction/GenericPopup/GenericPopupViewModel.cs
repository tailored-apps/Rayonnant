using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wise.Framework.Interface.ViewModel;
using Wise.Framework.Presentation.Commands;

namespace Wise.Framework.Presentation.UserInteraction.GenericPopup
{
    public class GenericPopupViewModel<T> : BindableBase, IGenericPopup<T>
    {
        public event PopupResponse<T> PopupClosed;
        
        private bool isOpen;
        public bool IsOpen
        {
            get { return isOpen; }
            set 
            { 
                SetProperty(ref isOpen, value);
                
                if (!isOpen)
                    RaiseCloseEvents(Confirmed);
            }
        }

        public bool confirmed;
        public bool Confirmed
        {
            get { return isOpen; }
            set { SetProperty(ref confirmed, value); }
        }
        public bool isEnabled;
        public bool IsEnabled
        {
            get { return isOpen; }
            set { SetProperty(ref isEnabled, value); }
        }

        private ICommand confirmedCommand;
        public ICommand ConfirmedCommand
        {
            get { return confirmedCommand; }
            set { SetProperty(ref confirmedCommand, value); }
        }

        private T viewModel;
        public T ViewModel
        {
            get { return viewModel; }
            set { SetProperty(ref viewModel, value); }
        }

        public bool IsConfirmationTextSpecyfied
        {
            get { return !string.IsNullOrEmpty(ConfirmationText); }
        }

        public bool IsCancellationTextSpecyfied
        {
            get { return !string.IsNullOrEmpty(CancellationText); }
        }

        private string confirmationText;
        public string ConfirmationText
        {
            get { return confirmationText; }
            set
            {
                SetProperty(ref confirmationText, value);
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("IsConfirmationTextSpecyfied"));
            }
        }

        private string cancellationText;
        public string CancellationText
        {
            get { return cancellationText; }
            set
            {
                SetProperty(ref cancellationText, value);
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("IsCancellationTextSpecyfied"));
            }
        }

        private string popupHeader;
        public string PopupHeader
        {
            get { return popupHeader; }
            set { SetProperty(ref popupHeader, value); }
        }

        public GenericPopupViewModel(T vm, string headerText)
        {
            ViewModel = vm;
            PopupHeader = headerText;
            ConfirmedCommand = new ConfirmPopupCommand<T>(this);
            IsEnabled = true;
        }
        
        public void Raise()
        {
            IsOpen = true;
        }

        private void RaiseCloseEvents(bool confirmed)
        {
            if (PopupClosed != null)
            {
                PopupClosed(ViewModel, confirmed);
            }
        }

        public void Dispose()
        {
            PopupClosed = null;
        }
    }
}
