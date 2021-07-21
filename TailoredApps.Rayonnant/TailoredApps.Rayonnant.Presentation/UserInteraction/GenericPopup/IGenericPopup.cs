using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TailoredApps.Rayonnant.Presentation.UserInteraction.GenericPopup
{
    public delegate void PopupResponse<T>(T value, bool confirmed);

    public interface IGenericPopup<T>
    {
        bool IsEnabled { get; }
        ICommand ConfirmedCommand { get; }
        T ViewModel { get; set; }
        void Raise();
        event PopupResponse<T> PopupClosed;
        string CancellationText { get; set; }
        bool Confirmed { get; set; }
        bool IsOpen { get; set; }
    }
}
