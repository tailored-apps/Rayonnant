using Microsoft.Practices.Prism.Regions;
using Wise.DummyModuleTwo.ViewModel;
using Wise.Framework.DependencyInjection;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Modularity;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Modularity;

namespace Wise.DummyModuleTwo
{
    public class DummyModuleTwo : ModuleBase<DummyModuleTwo>
    {
        private readonly IRegionManager regionManager;

        public DummyModuleTwo(IResourceManager resourceManager, IRegionManager regionManager, IMessanger messanger)
            : base(resourceManager,messanger)
        {
            this.regionManager = regionManager;

            messanger.Publish("publish from module two;");
        }


        protected override void RegisterResources()
        {
            ResourceManager.MergeResource("Wise.DummyModuleTwo;component/Resources/ViewTwoModelTemplates.xaml");
        }

        protected override void RegisterViewRegions()
        {
            
            regionManager.RegisterViewWithRegion(ShellRegionNames.ContentRegion, Container.Current.Resolve<ContentTwoViewModel>);
        }
    }
}
