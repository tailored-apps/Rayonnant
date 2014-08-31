using System;
using System.Windows.Input;
using Wise.DummyModuleTwo.Commands;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.InternalApplicationMessagning.Enum;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.DummyModuleTwo.ViewModel
{
    public class ContentTwoViewModel : ViewModelBase
    {

        private IMessanger messanger;
        public ContentTwoViewModel(IMessanger messanger)
        {

            this.messanger = messanger;
            messanger.Subscribe<string>(OnMessageArrived).ExecuteOn(MessageProcessingThread.Dispatcher );

            Button = new DummyCommandTwo(this, messanger);
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
            Label += DateTime.Now.ToString() + " HELLO: "+o;
        }
    }
}
