using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using Wise.DummyModuleTwo.ViewModel;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Presentation.Modularity;

namespace Wise.DummyModuleTwo.Commands
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
            messanger.Publish<NavigationRequest>(new NavigationRequest() { ViewModelFullName = "Wise.DummyModule.ViewModel.ContentViewModel",UriQuery = new NavigationParameters("?ASD=asd")});
            
        }
    }
}