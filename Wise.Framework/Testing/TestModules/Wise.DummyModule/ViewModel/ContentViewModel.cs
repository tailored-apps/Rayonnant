using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Regions;
using Wise.DummyModule.Commands;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.InternalApplicationMessagning.Enum;
using Wise.Framework.Presentation.Annotations;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.DummyModule.ViewModel
{
    [MenuItem("Modules|Dummy Module One", "Content View")]
    public class ContentViewModel : ViewModelBase
    {
        private string label;

        public ContentViewModel(IMessanger messanger)
        {
            //logger.Info("asd");
            messanger.Subscribe<string>(OnMessageArrived).ExecuteOn(MessageProcessingThread.Dispatcher);

            Button = new DummyCommand(this, messanger);
            base.Title = "asd";
            Items = new ObservableCollection<string>(new[] { "-- All --", "Anna", "annie", "cookie", "lolo test" });
            Reset = new ResetKey();
        }

        public ICommand Button { get; set; }

        public ICommand Reset { get; set; }

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


        private string selectedItem;

        public string SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }


        private ObservableCollection<string> items;

        public ObservableCollection<string> Items
        {
            get { return items; }
            set { SetProperty(ref items, value); }
        }
    }
}