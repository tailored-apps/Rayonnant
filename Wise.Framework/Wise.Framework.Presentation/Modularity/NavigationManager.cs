using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Regions;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Interface.Modularity;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.Framework.Presentation.Modularity
{
    public class NavigationManager : INavigationManager
    {
        private IDisposable subscription;
        private IMessanger messanger;
        private IRegionManager regionManager;
        public NavigationManager(IMessanger messanger, IRegionManager regionManager)
        {
            this.messanger = messanger;
            this.regionManager = regionManager;
            subscription = messanger.Subscribe<NavigationRequest>(OnMessageArrived);
        }

        private void OnMessageArrived(NavigationRequest obj)
        {

            regionManager.RequestNavigate(ShellRegionNames.ContentRegion, obj.ViewModelType.FullName, NavigationCompleted);

        }

        private void NavigationCompleted(NavigationResult obj)
        {
            //throw new NotImplementedException();
        }

        public void AddViewModel(ViewModelBase viewModel)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            subscription.Dispose();
        }
    }
}
