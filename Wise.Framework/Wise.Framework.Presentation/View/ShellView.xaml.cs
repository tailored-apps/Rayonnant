using System.Windows.Controls;
using Prism.Regions;
using Wise.Framework.DependencyInjection;

namespace Wise.Framework.Presentation.View
{
    /// <summary>
    ///     Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : UserControl
    {
        /// <summary>
        ///     Constructor, creates ShellView instance
        /// </summary>
        public ShellView()
        {
            InitializeComponent();

            RegionManager.SetRegionManager(this, Container.Current.Resolve<IRegionManager>());
            RegionManager.UpdateRegions();
        }
    }
}