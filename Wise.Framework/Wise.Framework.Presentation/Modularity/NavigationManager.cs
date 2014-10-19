using System;
using System.Windows;
using Common.Logging;
using Microsoft.Practices.Prism.Regions;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Window;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Interface.Modularity;
using Wise.Framework.Presentation.ViewModel;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.Presentation.Modularity
{
    public class NavigationManager : INavigationManager
    {
        private readonly IShellWindow shell;
        private readonly IDisposable subscription;
        private IMessanger messanger;
        private IRegionManager regionManager;
        private IContainer container;
        private ILog loger;
        public NavigationManager(IContainer container, IMessanger messanger, IRegionManager regionManager, IShellWindow shell)
        {
            this.loger = loger;
            this.container = container;
            this.shell = shell;
            this.messanger = messanger;
            this.regionManager = regionManager;
            subscription = messanger.Subscribe<NavigationRequest>(OnMessageArrived);
        }

        public void RegisterTypeForNavigation(Type viewModelType)
        {
            container.RegisterType(typeof(Object), viewModelType, viewModelType.FullName);
        }
        public void RegisterTypeForNavigation<T>()
        {
            container.RegisterType(typeof(Object), typeof(T), typeof(T).FullName);
        }

        public void RegisterViewModelForNavigation(ViewModelBase viewModel)
        {
            this.RegisterTypeForNavigation(viewModel.GetType());
        }

        private void OnMessageArrived(NavigationRequest obj)
        {
            var regManager = RegionManager.GetRegionManager(shell as DependencyObject);
            var reg = regManager.Regions[ShellRegionNames.ContentRegion];
            reg.RequestNavigate(obj.ViewModelType != null ? obj.ViewModelType.FullName : obj.ViewModelFullName, NavigationCompleted, obj.UriQuery);
        }

        private void NavigationCompleted(NavigationResult obj)
        {
            //loger.Info(string.Format("{0}{1}{2}", obj.Context, obj.Error, obj.Result));
        }

        public void Dispose()
        {
            subscription.Dispose();
        }
    }
}