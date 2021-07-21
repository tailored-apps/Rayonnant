using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Regions;
using TailoredApps.Rayonnant.DependencyInjection;
using TailoredApps.Rayonnant.Presentation.Commands;

namespace TailoredApps.Rayonnant.Presentation.View
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
            catch (Exception)
            {
            }
        }

        public static readonly DependencyProperty CloseItemCommandProperty = DependencyProperty.Register(
            "CloseItemCommand", typeof(ICommand), typeof(PrismViewBase), new PropertyMetadata(new CloseItemCommand()));

        public ICommand CloseItemCommand
        {
            get { return (ICommand)GetValue(CloseItemCommandProperty); }
            set { SetValue(CloseItemCommandProperty, value); }
        }

        public static readonly DependencyProperty TearOffCommandProperty = DependencyProperty.Register(
            "TearOffCommand", typeof (ICommand), typeof (PrismViewBase), new PropertyMetadata(new TearOffToggleCommand()));

        public ICommand TearOffCommand
        {
            get { return (ICommand) GetValue(TearOffCommandProperty); }
            set { SetValue(TearOffCommandProperty, value); }
        }
    }
}