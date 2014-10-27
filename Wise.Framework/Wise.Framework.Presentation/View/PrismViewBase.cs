using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using Wise.Framework.DependencyInjection;
using Wise.Framework.Presentation.Commands;

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