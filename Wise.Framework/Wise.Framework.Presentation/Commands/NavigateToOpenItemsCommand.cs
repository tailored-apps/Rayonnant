using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Modularity;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.Framework.Presentation.Commands
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
