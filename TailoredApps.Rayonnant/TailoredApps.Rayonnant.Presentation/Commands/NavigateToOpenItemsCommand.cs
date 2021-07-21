using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;
using TailoredApps.Rayonnant.Presentation.Interface;
using TailoredApps.Rayonnant.Presentation.Modularity;
using TailoredApps.Rayonnant.Presentation.ViewModel;

namespace TailoredApps.Rayonnant.Presentation.Commands
{
    public class NavigateToOpenItemsCommand : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var messanger = DependencyInjection.Container.Current.Resolve<IMessanger>();
            var preferenceManager = DependencyInjection.Container.Current.Resolve<IPreferenceManager>();
            messanger.Publish(new NavigationRequest() { ViewModelType = typeof(OpenItemsViewModel)});
        }

    }
}
