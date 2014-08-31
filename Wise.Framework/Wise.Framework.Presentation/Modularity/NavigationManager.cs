using System;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Modularity;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Window;
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
        private IShellWindow shell;
        public NavigationManager(IMessanger messanger, IRegionManager regionManager ,IShellWindow shell)
        {
            this.shell = shell;
            this.messanger = messanger;
            this.regionManager = regionManager;
            subscription = messanger.Subscribe<NavigationRequest>(OnMessageArrived);
        }

        private void OnMessageArrived(NavigationRequest obj)
        {
            IRegionManager regManager = RegionManager.GetRegionManager(shell as DependencyObject);
            IRegion reg = regManager.Regions[ShellRegionNames.ContentRegion];
            reg.RequestNavigate(obj.ViewModelType.FullName, NavigationCompleted);

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
