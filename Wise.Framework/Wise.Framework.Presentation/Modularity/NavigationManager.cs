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
        public NavigationManager(IContainer container, IMessanger messanger, IRegionManager regionManager, IShellWindow shell, ILog loger)
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
            var regionName = string.IsNullOrEmpty(obj.RegionName) ? ShellRegionNames.ContentRegion : obj.RegionName;
            var navigateTo = obj.ViewModelType != null ? obj.ViewModelType.FullName : obj.ViewModelFullName;
            regManager.Regions[regionName].RequestNavigate(navigateTo, NavigationCompleted, obj.UriQuery);
        }

        private void NavigationCompleted(NavigationResult obj)
        {
            var uri = obj.Context != null && obj.Context.Uri != null ? obj.Context.Uri.ToString() : string.Empty;
            var region = obj.Context != null && obj.Context.NavigationService != null && obj.Context.NavigationService.Region != null ? obj.Context.NavigationService.Region.Name : string.Empty;

            if (obj.Error != null)
            {
                loger.Info(string.Format("Navigation To: '{0}', has completed operation: '{1}', and is placed in region: '{2}', logged error: '{3}'", uri, obj.Result, region, obj.Error));
            }
            else
            {
                loger.Info(string.Format("Navigation To: '{0}', has completed operation: '{1}', and is placed in region: '{2}' without error", uri, obj.Result, region));
            }
        }

        public void Dispose()
        {
            subscription.Dispose();
        }
    }
}