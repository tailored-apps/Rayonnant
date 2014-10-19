using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Wise.Framework.DependencyInjection;

namespace Wise.Framework.Presentation.View
{
    public class PrismViewBase : UserControl
    {
        public PrismViewBase()
        {
            try
            {
                RegionManager.SetRegionManager(this, Container.Current.Resolve<IRegionManager>());
                RegionManager.UpdateRegions();
            }
            catch (Exception ex)
            {
            }
        }
    }
}