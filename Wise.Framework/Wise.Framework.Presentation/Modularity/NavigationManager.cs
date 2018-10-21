﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Common.Logging;
using Prism.Commands;
using Prism.Regions;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Security;
using Wise.Framework.Interface.Window;
using Wise.Framework.Presentation.Annotations;
using Wise.Framework.Presentation.Interface.Menu;
using Wise.Framework.Presentation.Interface.Modularity;
using Wise.Framework.Presentation.Interface.Shell;
using Wise.Framework.Presentation.ViewModel;
using Wise.Framework.Presentation.Window;

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
        private readonly IRegionNavigationJournal regionNavigationJournal;
        private readonly IDictionary<ViewModelBase, WindowBase> TearOffViewModels = new Dictionary<ViewModelBase, WindowBase>();
        private readonly List<Type> registeredViewModels;
        private readonly ISecurityService securityService;
        public NavigationManager(IContainer container, IMessanger messanger, IRegionManager regionManager,
            IShellWindow shell, ILog loger, IMenuService menuService, IRegionNavigationJournal regionNavigationJournal, ISecurityService securityService)
        {
            this.securityService = securityService;
            this.menuService = menuService;
            this.loger = loger;
            this.container = container;
            this.shell = shell;
            this.messanger = messanger;
            this.regionManager = regionManager;
            this.regionNavigationJournal = regionNavigationJournal;
            subscription = messanger.Subscribe<NavigationRequest>(OnMessageArrived);
            registeredViewModels = new List<Type>();
        }

        public void RegisterTypeForNavigation<T>()
        {
            RegisterTypeForNavigation(typeof(T));
        }

        public void RegisterTypeForNavigation(Type viewModelType)
        {
            loger.InfoFormat("Registering ViewModel For navigation: {0}", viewModelType);
            container.RegisterType(typeof(Object), viewModelType, viewModelType.FullName);
            AddMenuNavigation(viewModelType);
            registeredViewModels.Add(viewModelType);
        }



        public IEnumerable<ViewModelInfoAttribute> OpenedViewModelInfos
        {
            get
            {
                foreach (var view in regionManager.Regions[ShellRegionNames.ContentRegion].Views)
                {
                    if (view.GetType() != typeof(OpenItemsViewModel))
                        yield return GetViewModelInfoAttribute(view as ViewModelBase);
                }
            }
        }


        private ViewModelInfoAttribute GetViewModelInfoAttribute(ViewModelBase view)
        {
            var type = view.GetType();
            if (type != typeof(OpenItemsViewModel))
            {
                var attr = type.GetCustomAttributes(typeof(ViewModelInfoAttribute));

                if (attr != null && attr.Any())
                {
                    foreach (var attribute in attr)
                    {
                        var modelInfo = (ViewModelInfoAttribute)attribute;
                        modelInfo.ViewModelType = type;
                        modelInfo.ViewModel = view as ViewModelBase;
                        return modelInfo;
                    }
                }
            }
            return null;
        }
        private ViewModelInfoAttribute GetViewModelInfoAttribute(Type type)
        {
            if (type != typeof(OpenItemsViewModel))
            {
                var attr = type.GetCustomAttributes(typeof(ViewModelInfoAttribute));

                if (attr != null && attr.Any())
                {
                    foreach (var attribute in attr)
                    {
                        var modelInfo = (ViewModelInfoAttribute)attribute;
                        modelInfo.ViewModelType = type;
                        return modelInfo;
                    }
                }
            }
            return null;
        }

        public void CloseItem(ViewModelBase vm)
        {
            IRegion containingRegion = null;
            foreach (var region in regionManager.Regions)
            {
                if (region.Views.Contains(vm))
                {
                    containingRegion = region;
                    region.Remove(vm);
                }
            }

            if (TearOffViewModels.ContainsKey(vm))
            {
                TearOffViewModels[vm].Close();
                TearOffViewModels.Remove(vm);
            }

            if (containingRegion != null)
            {
                var view = containingRegion.Views.LastOrDefault();
                if (view != null)
                {
                    containingRegion.Activate(view);
                }
            }
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

            var security = false;
            try
            {
                var vmAttribute = obj.ViewModelType.GetCustomAttribute<ViewModelInfoAttribute>();
                if (vmAttribute != null)
                {
                    if (!string.IsNullOrEmpty(vmAttribute.AllowedRoles))
                    {
                        var listOfRoles = vmAttribute.AllowedRoles.Split(new[] { ';', ':', '/', ',' });
                        if (listOfRoles.Any())
                        {
                            if (listOfRoles.Contains("*"))
                            {
                                security = true;
                            }
                            else
                            {
                                if (listOfRoles.Any(role => securityService.IsInRole(role)))
                                {
                                    security = true;
                                }
                            }

                        }
                    }
                    else
                    {
                        security = true;
                    }
                }
            }
            catch (Exception ex)
            {
                security = true;
                loger.Error(ex);

            }
            if (security)
            {
                regManager.Regions[regionName].RequestNavigate(navigateTo, NavigationCompleted, obj.UriQuery);
            }
            else
            {
                MessageBox.Show("Wypad nie ma roli");
            }


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
                loger.InfoFormat("Navigation To: '{0}', has completed operation: '{1}', and is placed in region: '{2}', logged error: '{3}'", uri, obj.Result, region, obj.Error);
            }
            else
            {
                loger.InfoFormat("Navigation To: '{0}', has completed operation: '{1}', and is placed in region: '{2}' without error", uri, obj.Result, region);
            }

        }

        private void AddMenuNavigation(Type viewModel)
        {
            var attr = viewModel.GetCustomAttributes(typeof(MenuItemAttribute));

            if (attr != null && attr.Any())
            {
                foreach (Attribute attribute in attr)
                {
                    var menuItem = (MenuItemAttribute)attribute;
                    var command = new DelegateCommand(() => OnMessageArrived(new NavigationRequest { ViewModelType = viewModel, UriQuery = new NavigationParameters(menuItem.NavigationParameters) }));
                    loger.InfoFormat("Adding ViewModel: '{0}' For navigation from menu: '{1}'", viewModel, menuItem.Path);
                    menuService.AddMenuItem(new System.Windows.Controls.MenuItem { Header = menuItem.DisplayName, Command = command }, menuItem.Path);
                }
            }
        }


        public void TearOff(ViewModelBase vm)
        {
            if (!vm.IsTearOff)
            {
                var modalWindow = new ModalWindow { DataContext = vm, };

                modalWindow.Show();
                vm.IsTearOff = true;
                TearOffViewModels.Add(vm, modalWindow);

                foreach (var region in regionManager.Regions)
                {
                    IRegion containingRegion = null;
                    if (region.ActiveViews.Contains(vm))
                    {
                        containingRegion = region;
                        region.Remove(vm);
                    }
                    if (containingRegion != null)
                    {
                        var view = containingRegion.Views.LastOrDefault();
                        if (view != null)
                        {
                            containingRegion.Activate(view);
                        }
                    }
                }

            }
        }


        public void Dock(ViewModelBase vm)
        {
            if (vm.IsTearOff)
            {
                var modalWindow = TearOffViewModels[vm];

                modalWindow.Close();
                vm.IsTearOff = false;
                TearOffViewModels.Remove(vm);

                var region = regionManager.Regions[ShellRegionNames.ContentRegion];

                region.Add(vm);
                region.Activate(vm);

            }
        }


        public IEnumerable<ViewModelInfoAttribute> RegisteredViewModels
        {
            get
            {
                return registeredViewModels.Where(x => x != typeof(OpenItemsViewModel)).Select(x => GetViewModelInfoAttribute(x));
            }
        }
    }
}