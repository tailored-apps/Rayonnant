using Prism.Regions;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Modularity;
using Wise.Framework.Presentation.Interface.Modularity;
using Wise.Framework.Presentation.Interface.Shell;
using Wise.Framework.Presentation.Modularity;
using Wise.Framework.SecurityModule.ViewModel;

namespace Wise.Framework.SecurityModule
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
            ResourceManager.MergeResource("Wise.Framework.SecurityModule;component/Resources/ViewModelTemplates.xaml");
        }

        protected override void RegisterViewRegions()
        {
            regionManager.RegisterViewWithRegion(ShellRegionNames.ContentRegion, typeof (ApplicationsViewModel));
        }
    }
}
