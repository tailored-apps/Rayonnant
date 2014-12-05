using System;
using System.Windows.Input;
using Wise.DummyModule.ViewModel;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Presentation.Modularity;

namespace Wise.DummyModule.Commands
{
    public class ResetKey : ICommand
    {
       

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
            var combo = parameter as CustomDropDown;
            if(combo !=null)
            {
                
            }
        }
    }
}