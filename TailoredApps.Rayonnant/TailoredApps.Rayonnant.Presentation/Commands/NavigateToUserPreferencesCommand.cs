using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;
using TailoredApps.Rayonnant.Presentation.Interface;
using TailoredApps.Rayonnant.Presentation.Modularity;

namespace TailoredApps.Rayonnant.Presentation.Commands
{
    public class NavigateToUserPreferencesCommand : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var messanger = DependencyInjection.Container.Current.Resolve<IMessanger>();
            messanger.Publish(new NavigationRequest() { ViewModelFullName = "TailoredApps.Rayonnant.Presentation.ViewModel.UserPreferencesSettingsViewModel"});
        }

    }
}
