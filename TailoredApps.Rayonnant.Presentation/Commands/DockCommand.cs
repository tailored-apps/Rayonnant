using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TailoredApps.Rayonnant.DependencyInjection;
using TailoredApps.Rayonnant.Presentation.Interface.Modularity;
using TailoredApps.Rayonnant.Presentation.ViewModel;

namespace TailoredApps.Rayonnant.Presentation.Commands
{
    public class DockCommand : BaseCommand
    {
       public override bool CanExecute(object parameter)
       {
           return true;
       }

       public override void Execute(object parameter)
       {
           var vm = parameter as ViewModelBase;
           if (vm != null)
           {
               var manager = Container.Current.Resolve<INavigationManager>();
               manager.Dock(vm);
           }
       }

    }
}
