using System.Windows.Controls;
using Prism.Regions;
using TailoredApps.Rayonnant.DependencyInjection;

namespace TailoredApps.Rayonnant.Presentation.View
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