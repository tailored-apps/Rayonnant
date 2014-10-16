using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Wise.Framework.Presentation.View
{
    public  class PrismViewBase : UserControl
    {
        public PrismViewBase()
        {
            try
            {
                RegionManager.SetRegionManager(this, DependencyInjection.Container.Current.Resolve<IRegionManager>());
                RegionManager.UpdateRegions();
            }
            catch (Exception ex)
            {
                
            }
           
            
        }
    }
}
