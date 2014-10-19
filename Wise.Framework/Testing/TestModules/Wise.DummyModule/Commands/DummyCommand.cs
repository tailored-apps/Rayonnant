using System;
using System.Windows.Input;
using Wise.DummyModule.ViewModel;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Presentation.Modularity;

namespace Wise.DummyModule.Commands
{
    public class DummyCommand : ICommand
    {
        private readonly IMessanger messanger;
        private ContentViewModel vm;

        public DummyCommand(ContentViewModel vm, IMessanger messanger)
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
            messanger.Publish(new NavigationRequest {ViewModelType = typeof (OtherContentViewModel)});
        }
    }
}