using System;
using System.Windows.Input;
using Wise.DummyModule.Commands;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.InternalApplicationMessagning.Enum;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.DummyModule.ViewModel
{
    public class ContentViewModel : ViewModelBase
    {
        public ContentViewModel(IMessanger messanger)
        {
            messanger.Subscribe<string>(OnMessageArrived).ExecuteOn(MessageProcessingThread.Dispatcher );

            Button = new DummyCommand(this, messanger);
        }

        public ICommand Button { get; set; }

        private string label;
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
            Label += DateTime.Now + " HELLO: "+o;
        }
    }
}
