using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TailoredApps.Rayonnant.DependencyInjection;
using TailoredApps.Rayonnant.Interface.ViewModel;
using TailoredApps.Rayonnant.Presentation.Interface.Modularity;
using TailoredApps.Rayonnant.Presentation.UserInteraction.GenericPopup;
using TailoredApps.Rayonnant.Presentation.ViewModel;

namespace TailoredApps.Rayonnant.Presentation.Commands
{
    public class ConfirmPopupCommand<T> : BaseCommand
    {
        private IGenericPopup<T> GenericPopup;

        private bool isValid = true;
        public ConfirmPopupCommand(IGenericPopup<T> GenericPopup)
        {
            this.GenericPopup = GenericPopup;
        }

        public override bool CanExecute(object parameter)
        {
            var val = GenericPopup.ViewModel as IValidableViewModel;
            if (val != null)
            {
                return val.Validate();
            }

            return true;
        }

        public override void Execute(object parameter)
        {
            var val = GenericPopup.ViewModel as IValidableViewModel;
            if (val != null)
            {
                isValid = val.Validate();
            }

            if (isValid)
            {
                GenericPopup.Confirmed= true;
                GenericPopup.IsOpen = false;
            }
        }

    }
}
