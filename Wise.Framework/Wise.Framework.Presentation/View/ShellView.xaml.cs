using System.ComponentModel;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Wise.Framework.DependencyInjection;
namespace Wise.Framework.Presentation.View
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : UserControl
    {
        /// <summary>
        /// Constructor, creates ShellView instance
        /// </summary>
        public ShellView()
        {
            InitializeComponent();
            RegionManager.SetRegionManager(this, DependencyInjection.Container.Current.Resolve<IRegionManager>());
            RegionManager.UpdateRegions();
        }
    }
}
