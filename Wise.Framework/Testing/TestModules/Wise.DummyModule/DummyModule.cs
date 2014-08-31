using System.Reflection;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wise.DummyModule.ViewModel;
using Wise.Framework.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Modularity;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Modularity;

namespace Wise.DummyModule
{
    public class DummyModule : ModuleBase<DummyModule>
    {
        private readonly IRegionManager regionManager;

        public DummyModule(IResourceManager resourceManager, IRegionManager regionManager, IMessanger messanger, IContainer container)
            : base(resourceManager, messanger,container)
        {
            this.regionManager = regionManager;

            messanger.Publish("publish from module;");
        }

        protected override void RegisterServices()
        {
            base.RegisterServices();
            Container.RegisterTypeForNavigation<ContentViewModel>();
            Container.RegisterTypeForNavigation<OtherContentViewModel>();
        }


        protected override void RegisterResources()
        {
            ResourceManager.MergeResource("Wise.DummyModule;component/Resources/ViewModelTemplates.xaml");
            Container.RegisterTypeForNavigation<ContentViewModel>();
            Container.RegisterTypeForNavigation<OtherContentViewModel>();
        }

        protected override void RegisterViewRegions()
        {

            regionManager.RegisterViewWithRegion(ShellRegionNames.ContentRegion, typeof(ContentViewModel));
            regionManager.RegisterViewWithRegion(ShellRegionNames.ContentRegion, typeof(OtherContentViewModel));
        }
    }
}
