using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Common.Logging;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Practices.Prism.Regions;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Window;
using Wise.Framework.Presentation.Annotations;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Interface.Menu;
using Wise.Framework.Presentation.Interface.Modularity;
using Wise.Framework.Presentation.Services;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.Framework.Presentation.Modularity
{
    public class NavigationManager : INavigationManager
    {
        private readonly IContainer container;
        private readonly ILog loger;
        private readonly IMenuService menuService;
        private readonly IMessanger messanger;
        private readonly IRegionManager regionManager;
        private readonly IShellWindow shell;
        private readonly IDisposable subscription;

        public NavigationManager(IContainer container, IMessanger messanger, IRegionManager regionManager,
            IShellWindow shell, ILog loger, IMenuService menuService)
        {
            this.menuService = menuService;
            this.loger = loger;
            this.container = container;
            this.shell = shell;
            this.messanger = messanger;
            this.regionManager = regionManager;
            subscription = messanger.Subscribe<NavigationRequest>(OnMessageArrived);
        }

        public void RegisterTypeForNavigation(Type viewModelType)
        {
            container.RegisterType(typeof (Object), viewModelType, viewModelType.FullName);
            AddMenuNavigation(viewModelType);
        }

        public void RegisterTypeForNavigation<T>()
        {
            container.RegisterType(typeof (Object), typeof (T), typeof (T).FullName);
            AddMenuNavigation(typeof (T));
        }

        public void RegisterViewModelForNavigation(ViewModelBase viewModel)
        {
            RegisterTypeForNavigation(viewModel.GetType());
        }

        public void Dispose()
        {
            subscription.Dispose();
        }

        private void OnMessageArrived(NavigationRequest obj)
        {
            IRegionManager regManager = RegionManager.GetRegionManager(shell as DependencyObject);
            string regionName = string.IsNullOrEmpty(obj.RegionName) ? ShellRegionNames.ContentRegion : obj.RegionName;
            string navigateTo = obj.ViewModelType != null ? obj.ViewModelType.FullName : obj.ViewModelFullName;
            regManager.Regions[regionName].RequestNavigate(navigateTo, NavigationCompleted, obj.UriQuery);
        }

        private void NavigationCompleted(NavigationResult obj)
        {
            string uri = obj.Context != null && obj.Context.Uri != null ? obj.Context.Uri.ToString() : string.Empty;
            string region = obj.Context != null && obj.Context.NavigationService != null &&
                            obj.Context.NavigationService.Region != null
                ? obj.Context.NavigationService.Region.Name
                : string.Empty;

            if (obj.Error != null)
            {
                loger.Info(
                    string.Format(
                        "Navigation To: '{0}', has completed operation: '{1}', and is placed in region: '{2}', logged error: '{3}'",
                        uri, obj.Result, region, obj.Error));
            }
            else
            {
                loger.Info(
                    string.Format(
                        "Navigation To: '{0}', has completed operation: '{1}', and is placed in region: '{2}' without error",
                        uri, obj.Result, region));
            }
        }

        private void AddMenuNavigation(Type viewModel)
        {
            IEnumerable<Attribute> attr = viewModel.GetCustomAttributes(typeof (MenuItem));

            if (attr != null && attr.Count() > 0)
            {
                foreach (Attribute attribute in attr)
                {
                    var command =
                        new ActionCommand(() => OnMessageArrived(new NavigationRequest {ViewModelType = viewModel}));
                    var menuItem = (MenuItem) attribute;
                    menuService.AddMenuItem(
                        new System.Windows.Controls.MenuItem {Header = menuItem.DisplayName, Command = command},
                        string.Concat(MenuService.MENU_ITEMS_PREFIX, menuItem.Path));
                }
            }
        }
    }
}