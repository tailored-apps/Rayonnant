using System;
using System.Windows.Input;
using TailoredApps.Rayonnant.DummyModule.ViewModel;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;
using TailoredApps.Rayonnant.Presentation.Modularity;

namespace TailoredApps.Rayonnant.DummyModule.Commands
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