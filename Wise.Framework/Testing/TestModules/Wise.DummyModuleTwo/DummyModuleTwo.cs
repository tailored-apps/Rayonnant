using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Wise.DummyModuleTwo.ViewModel;
using Wise.Framework.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Modularity;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Modularity;

namespace Wise.DummyModuleTwo
{
    public class DummyModuleTwo : ModuleBase<DummyModuleTwo>
    {
        private readonly IRegionManager regionManager;

        public DummyModuleTwo(IResourceManager resourceManager, IRegionManager regionManager, IMessanger messanger, IContainer container)
            : base(resourceManager, messanger, container)
        {
            this.regionManager = regionManager;

            messanger.Publish("publish from module two;");
        }


        protected override void RegisterResources()
        {
            ResourceManager.MergeResource("Wise.DummyModuleTwo;component/Resources/ViewTwoModelTemplates.xaml");

            Container.RegisterTypeForNavigation<ContentTwoViewModel>();
        }


    
        protected override void RegisterViewRegions()
        {
            regionManager.RegisterViewWithRegion(ShellRegionNames.ContentRegion,typeof(ContentTwoViewModel) );
        }
    }
}
