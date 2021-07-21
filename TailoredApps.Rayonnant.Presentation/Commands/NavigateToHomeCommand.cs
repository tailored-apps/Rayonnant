using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;
using TailoredApps.Rayonnant.Presentation.Interface;
using TailoredApps.Rayonnant.Presentation.Modularity;

namespace TailoredApps.Rayonnant.Presentation.Commands
{
    public class NavigateToHomeCommand :BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var messanger = DependencyInjection.Container.Current.Resolve<IMessanger>();
            var preferenceManager = DependencyInjection.Container.Current.Resolve<IPreferenceManager>();
            
            var home =preferenceManager.GetUserHomeView();
            if (string.IsNullOrEmpty(home))
            {
                home = "TailoredApps.Rayonnant.Presentation.ViewModel.UserPreferencesSettingsViewModel";
            }

            messanger.Publish(new NavigationRequest() { ViewModelFullName = home});
        }

    }
}
