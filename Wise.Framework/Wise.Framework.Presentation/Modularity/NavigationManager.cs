using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Regions;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Presentation.Interface.Modularity;

namespace Wise.Framework.Presentation.Modularity
{
    public class NavigationManager : INavigationManager
    {
        private IMessanger Messanger;
        private IRegionManager regionManager;

        public NavigationManager(IMessanger Messanger, IRegionManager regionManager)
        {
            // TODO: Complete member initialization
            this.Messanger = Messanger;
            this.regionManager = regionManager;
        }

        public void AddViewModel(ViewModel.ViewModelBase viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
