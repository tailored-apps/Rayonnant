using System;
using System.Windows.Input;
using TailoredApps.Rayonnant.DummyModule.Commands;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning.Enum;
using TailoredApps.Rayonnant.Presentation.Annotations;
using TailoredApps.Rayonnant.Presentation.ViewModel;

namespace TailoredApps.Rayonnant.DummyModule.ViewModel
{
    [ViewModelInfo(DisplayName ="Dummy Module One Other Content View Model",MenuGroup = "Tools")]
    [MenuItem(Path ="Modules|Dummy Module One", DisplayName ="Other Content View Model")]
    public class OtherContentViewModel : ViewModelBase
    {
        private string label;

        public OtherContentViewModel(IMessanger messanger)
        {
            messanger.Subscribe<string>(OnMessageArrived).ExecuteOn(MessageProcessingThread.Dispatcher);

            Button = new DummyCommandTwo(this, messanger);
            base.Title = "asd";
        }

        public ICommand Button { get; set; }

        public string Label
        {
            get { return label; }
            protected set
            {
                label = value;
                OnPropertyChanged("Label");
            }
        }

        private void OnMessageArrived(string o)
        {
            Label += DateTime.Now + " HELLO: " + o;
        }
    }
}