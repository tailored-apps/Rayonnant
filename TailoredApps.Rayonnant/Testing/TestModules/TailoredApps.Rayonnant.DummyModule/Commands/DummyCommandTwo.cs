using System;
using System.Windows.Input;
using TailoredApps.Rayonnant.DummyModule.ViewModel;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;
using TailoredApps.Rayonnant.Presentation.Modularity;

namespace TailoredApps.Rayonnant.DummyModule.Commands
{
    public class DummyCommandTwo : ICommand
    {
        private readonly IMessanger messanger;
        private OtherContentViewModel vm;

        public DummyCommandTwo(OtherContentViewModel vm, IMessanger messanger)
        {
            this.messanger = messanger;
            this.vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            messanger.Publish("publish from buton on view model;");
            messanger.Publish(new NavigationRequest
            {
                ViewModelFullName = "TailoredApps.Rayonnant.DummyModuleTwo.ViewModel.ContentTwoViewModel"
            });
        }
    }
}