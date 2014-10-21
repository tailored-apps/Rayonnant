using System;
using Microsoft.Practices.Prism.Regions;
using Wise.DummyModule.ViewModel;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Modularity;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Interface.Modularity;
using Wise.Framework.Presentation.Modularity;

namespace Wise.DummyModule
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
            ResourceManager.MergeResource("Wise.DummyModule;component/Resources/ViewModelTemplates.xaml");
        }

        protected override void RegisterViewRegions()
        {
            regionManager.RegisterViewWithRegion(ShellRegionNames.ContentRegion, typeof (ContentViewModel));
            regionManager.RegisterViewWithRegion(ShellRegionNames.ContentRegion, typeof (OtherContentViewModel));
        }
    }
}