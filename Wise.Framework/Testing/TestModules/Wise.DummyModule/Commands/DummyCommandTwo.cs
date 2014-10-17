using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wise.DummyModule.ViewModel;
using Wise.DummyModuleTwo.ViewModel;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Presentation.Modularity;

namespace Wise.DummyModule.Commands
{
    public class DummyCommandTwo : ICommand
    {
        private IMessanger messanger;
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
            messanger.Publish<NavigationRequest>(new NavigationRequest() { ViewModelFullName = "Wise.DummyModuleTwo.ViewModel.ContentTwoViewModel" });

            
        }
    }
}
