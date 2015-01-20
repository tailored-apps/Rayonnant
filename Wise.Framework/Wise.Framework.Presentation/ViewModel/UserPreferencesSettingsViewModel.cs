using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Wise.Framework.Presentation.Annotations;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Interface.Modularity;

namespace Wise.Framework.Presentation.ViewModel
{
    [ViewModelInfo("User Preferences", "Settings", 0)]
    [MenuItem("Settings", "User Preferences")]
    public class UserPreferencesSettingsViewModel : ViewModelBase
    {
        private readonly IPreferenceManager preferenceManager;
        private readonly INavigationManager navigationManager;
        public UserPreferencesSettingsViewModel(IPreferenceManager preferenceManager, INavigationManager navigationManager)
        {
            this.preferenceManager = preferenceManager;
            this.navigationManager = navigationManager;
            Title = "User Preferences";
            Icon = new Uri("pack://application:,,,/Wise.Framework.Presentation.Resources;component/Resources/service-128.ico", UriKind.Absolute);
            SaveCommand = new DelegateCommand(SaveData);

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
            if (SelectedHomeView != null)
                preferenceManager.SavePreference("HomeView", SelectedHomeView.ViewModelType);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            ViewModels = new ObservableCollection<ViewModelInfoAttribute>(navigationManager.RegisteredViewModels);
            var home = preferenceManager.GetUserHomeView();
            SelectedHomeView = navigationManager.RegisteredViewModels.SingleOrDefault(x => string.Equals(x.ViewModelType.ToString(), home));
        }
    }
}
