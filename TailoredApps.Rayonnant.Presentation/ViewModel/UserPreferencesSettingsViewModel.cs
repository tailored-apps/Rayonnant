using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Regions;
using TailoredApps.Rayonnant.Presentation.Annotations;
using TailoredApps.Rayonnant.Presentation.Interface;
using TailoredApps.Rayonnant.Presentation.Interface.Modularity;

namespace TailoredApps.Rayonnant.Presentation.ViewModel
{
    [ViewModelInfo(DisplayName = "User Preferences", MenuGroup = "Settings", SecurityLevel = 0)]
    [MenuItem(Path ="Settings", DisplayName = "User Preferences")]
    public class UserPreferencesSettingsViewModel : ViewModelBase
    {
        private readonly IPreferenceManager preferenceManager;
        private readonly INavigationManager navigationManager;
        public UserPreferencesSettingsViewModel(IPreferenceManager preferenceManager, INavigationManager navigationManager)
        {
            this.preferenceManager = preferenceManager;
            this.navigationManager = navigationManager;
            Title = "User Preferences";
            Icon = new Uri("pack://application:,,,/TailoredApps.Rayonnant.Presentation.Resources;component/Resources/service-128.ico", UriKind.Absolute);
            SaveCommand = new DelegateCommand(SaveData);
            base.ScreenId = string.Empty;

        }

        private ObservableCollection<ViewModelInfoAttribute> viewModels;

        public ObservableCollection<ViewModelInfoAttribute> ViewModels
        {
            get { return viewModels; }
            set { SetProperty(ref viewModels, value); }
        }

        private ViewModelInfoAttribute selectedHomeView;

        public ViewModelInfoAttribute SelectedHomeView
        {
            get { return selectedHomeView; }
            set { SetProperty(ref selectedHomeView, value); }
        }

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }
            set { SetProperty(ref saveCommand, value); }
        }

        private void SaveData()
        {
            if (SelectedHomeView != null && !string.IsNullOrEmpty(SelectedHomeView.DisplayName))
            {
                preferenceManager.SavePreference("HomeView", SelectedHomeView.ViewModelType.ToString());

            }
            else
            {
                preferenceManager.SavePreference("HomeView", string.Empty);
            }
            navigationManager.CloseItem(this);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            ViewModels = new ObservableCollection<ViewModelInfoAttribute>(navigationManager.RegisteredViewModels);
            var vmia = new ViewModelInfoAttribute();
            vmia.ViewModelType = typeof(string);
            ViewModels.Insert(0, vmia);
            var home = preferenceManager.GetUserHomeView();
            SelectedHomeView = navigationManager.RegisteredViewModels.SingleOrDefault(x => string.Equals(x.ViewModelType.ToString(), home));

        }
    }
}
