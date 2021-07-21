using Prism.Regions;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;
using TailoredApps.Rayonnant.Presentation.Annotations;
using TailoredApps.Rayonnant.Presentation.Interface.Modularity;
using TailoredApps.Rayonnant.Presentation.Modularity;
using TailoredApps.Rayonnant.Presentation.ViewModel;

namespace TailoredApps.Rayonnant.Presentation.Commands
{
    public class NavigateToViewModelCommand : BaseCommand
    {
        private readonly OpenItemsViewModel openItemsViewModel;
        public NavigateToViewModelCommand(OpenItemsViewModel openItemsViewModel)
        {
            this.openItemsViewModel = openItemsViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var manager = DependencyInjection.Container.Current.Resolve<INavigationManager>();
            manager.CloseItem(openItemsViewModel);
            var vm = parameter as ViewModelInfoAttribute;
            var messanger = DependencyInjection.Container.Current.Resolve<IMessanger>();
            messanger.Publish(new NavigationRequest() { ViewModelType = vm.ViewModelType, UriQuery = new NavigationParameters(string.Format("ScreenId={0}", vm.ViewModel.ScreenId)) });

        }

    }
}
