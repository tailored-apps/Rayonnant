using System;
using System.Windows.Input;
using Prism.Regions;
using TailoredApps.Rayonnant.DummyModuleTwo.ViewModel;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;
using TailoredApps.Rayonnant.Presentation.Modularity;

namespace TailoredApps.Rayonnant.DummyModuleTwo.Commands
{
    public class DummyCommandTwo : ICommand
    {
        private readonly IMessanger messanger;
        private ContentTwoViewModel vm;

        public DummyCommandTwo(ContentTwoViewModel vm, IMessanger messanger)
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
                ViewModelFullName = "TailoredApps.Rayonnant.DummyModule.ViewModel.ContentViewModel",
                UriQuery = new NavigationParameters("?ASD=asd")
            });
        }
    }
}