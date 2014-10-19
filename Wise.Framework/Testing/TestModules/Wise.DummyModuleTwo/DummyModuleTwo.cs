using Microsoft.Practices.Prism.Regions;
using Wise.DummyModuleTwo.ViewModel;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Modularity;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Interface.Modularity;
using Wise.Framework.Presentation.Modularity;

namespace Wise.DummyModuleTwo
{
    public class DummyModuleTwo : ModuleBase<DummyModuleTwo>
    {
        private readonly IRegionManager regionManager;
        private INavigationManager navigationManager;

        public DummyModuleTwo(IResourceManager resourceManager, IRegionManager regionManager, INavigationManager navigationManager, IMessanger messanger,
            IContainer container)
            : base(resourceManager, messanger, container)
        {
            this.regionManager = regionManager;
            this.navigationManager = navigationManager;
            messanger.Publish("publish from module two;");
        }

        

        protected override void RegisterResources()
        {
            ResourceManager.MergeResource("Wise.DummyModuleTwo;component/Resources/ViewTwoModelTemplates.xaml");

            navigationManager.RegisterTypeForNavigation<ContentTwoViewModel>();
        }


        protected override void RegisterViewRegions()
        {
            regionManager.RegisterViewWithRegion(ShellRegionNames.ContentRegion, typeof (ContentTwoViewModel));
        }
    }
}