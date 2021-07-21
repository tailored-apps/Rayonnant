
using TailoredApps.Rayonnant.DummyModuleTwo.ViewModel;
using TailoredApps.Rayonnant.Interface.DependencyInjection;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;
using TailoredApps.Rayonnant.Interface.Modularity;
using TailoredApps.Rayonnant.Presentation.Interface;
using TailoredApps.Rayonnant.Presentation.Interface.Modularity;
using TailoredApps.Rayonnant.Presentation.Modularity;

namespace TailoredApps.Rayonnant.DummyModuleTwo
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
            ResourceManager.MergeResource("TailoredApps.Rayonnant.DummyModuleTwo;component/Resources/ViewTwoModelTemplates.xaml");

            navigationManager.RegisterTypeForNavigation<ContentTwoViewModel>();
        }

        protected override void RegisterViewRegions()
        {
            //regionManager.RegisterViewWithRegion(ShellRegionNames.ContentRegion, typeof (ContentTwoViewModel));
        }
    }
}