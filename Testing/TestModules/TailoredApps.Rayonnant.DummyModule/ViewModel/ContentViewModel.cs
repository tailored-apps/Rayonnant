using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Prism.Regions;
using TailoredApps.Rayonnant.DummyModule.Commands;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning.Enum;
using TailoredApps.Rayonnant.Presentation.Annotations;
using TailoredApps.Rayonnant.Presentation.ViewModel;

namespace TailoredApps.Rayonnant.DummyModule.ViewModel
{
    [ViewModelInfo(DisplayName ="Dummy Module One Content View", MenuGroup ="Tools", SecurityLevel =0)]
    [MenuItem(Path ="Modules|Dummy Module One", DisplayName ="Content View")]
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