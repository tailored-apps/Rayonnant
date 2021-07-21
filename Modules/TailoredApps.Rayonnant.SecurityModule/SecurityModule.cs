using Prism.Regions;
using TailoredApps.Rayonnant.Interface.DependencyInjection;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;
using TailoredApps.Rayonnant.Interface.Modularity;
using TailoredApps.Rayonnant.Presentation.Interface.Modularity;
using TailoredApps.Rayonnant.Presentation.Interface.Shell;
using TailoredApps.Rayonnant.Presentation.Modularity;
using TailoredApps.Rayonnant.SecurityModule.ViewModel;

namespace TailoredApps.Rayonnant.SecurityModule
{
    public class SecurityModule : ModuleBase<SecurityModule>
    {
        private readonly INavigationManager navigationManager;
        private readonly IRegionManager regionManager;
        public SecurityModule(IResourceManager resourceManager, IMessanger messanger, IContainer container, INavigationManager navigationManager, IRegionManager regionManager)
            : base(resourceManager, messanger, container)
        {
            this.navigationManager = navigationManager;
            this.regionManager = regionManager;
        }

        protected override void RegisterServices()
        {
            base.RegisterServices();
            navigationManager.RegisterTypeForNavigation<ApplicationsViewModel>();
        }

        protected override void RegisterResources()
        {
            ResourceManager.MergeResource("TailoredApps.Rayonnant.SecurityModule;component/Resources/ViewModelTemplates.xaml");
        }

        protected override void RegisterViewRegions()
        {
            regionManager.RegisterViewWithRegion(ShellRegionNames.ContentRegion, typeof (ApplicationsViewModel));
        }
    }
}
