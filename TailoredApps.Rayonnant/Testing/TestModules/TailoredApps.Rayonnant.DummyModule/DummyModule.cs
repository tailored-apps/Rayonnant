using System;
using Prism.Regions;
using TailoredApps.Rayonnant.DummyModule.ViewModel;
using TailoredApps.Rayonnant.Interface.DependencyInjection;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;
using TailoredApps.Rayonnant.Interface.Modularity;
using TailoredApps.Rayonnant.Presentation.Interface;
using TailoredApps.Rayonnant.Presentation.Interface.Modularity;
using TailoredApps.Rayonnant.Presentation.Interface.Shell;
using TailoredApps.Rayonnant.Presentation.Modularity;

namespace TailoredApps.Rayonnant.DummyModule
{
    public class DummyModule : ModuleBase<DummyModule>
    {
        private readonly INavigationManager navigationManager;
        private readonly IRegionManager regionManager;

        public DummyModule(IResourceManager resourceManager, INavigationManager navigationManager,
            IRegionManager regionManager, IMessanger messanger, IContainer container)
            : base(resourceManager, messanger, container)
        {
            this.navigationManager = navigationManager;
            this.regionManager = regionManager;
            messanger.Publish("publish from module;");
        }

        protected override void RegisterServices()
        {
            base.RegisterServices();
            navigationManager.RegisterTypeForNavigation<ContentViewModel>();
            navigationManager.RegisterTypeForNavigation<OtherContentViewModel>();
        }

        protected override void RegisterResources()
        {
            ResourceManager.MergeResource("TailoredApps.Rayonnant.DummyModule;component/Resources/ViewModelTemplates.xaml");
        }

        protected override void RegisterViewRegions()
        {
            regionManager.RegisterViewWithRegion(ShellRegionNames.ContentRegion, typeof (ContentViewModel));
            regionManager.RegisterViewWithRegion(ShellRegionNames.ContentRegion, typeof (OtherContentViewModel));
        }
    }
}