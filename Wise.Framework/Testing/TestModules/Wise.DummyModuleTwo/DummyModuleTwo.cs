
using Wise.DummyModuleTwo.ViewModel;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Modularity;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Interface.Modularity;
using Wise.Framework.Presentation.Interface.Shell;
using Wise.Framework.Presentation.Modularity;

namespace Wise.DummyModuleTwo
{
    public class DummyModuleTwo : ModuleBase<DummyModuleTwo>
    {
        private readonly INavigationManager navigationManager;

        public DummyModuleTwo(IResourceManager resourceManager, 
            INavigationManager navigationManager, IMessanger messanger,
            IContainer container)
            : base(resourceManager, messanger, container)
        {
            this.navigationManager = navigationManager;
            messanger.Publish("publish from module two;");
        }


        protected override void RegisterResources()
        {
            ResourceManager.MergeResource("Wise.DummyModuleTwo.Core;component/Resources/ViewTwoModelTemplates.xaml");

            navigationManager.RegisterTypeForNavigation<ContentTwoViewModel>();
        }


        protected override void RegisterViewRegions()
        {
            //regionManager.RegisterViewWithRegion(ShellRegionNames.ContentRegion, typeof (ContentTwoViewModel));
        }
    }
}