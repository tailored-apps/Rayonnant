using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wise.Framework.DependencyInjection;
using Wise.Framework.Presentation.Interface.Modularity;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.Framework.Presentation.Commands
{
    public class CloseItemCommand : BaseCommand
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
               manager.CloseItem(vm);
           }
       }

      
    }
}
