using System;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.Presentation.Interface.Modularity
{
    /// <summary>
    ///     Interface used to region configuration service
    /// </summary>
    public interface IRegionConfigurator
    {
        /// <summary>
        ///     method configures container
        /// </summary>
        /// <param name="container">container to configure</param>
        void ConfigureContainer(IContainer container);

        /// <summary>
        ///     method configures regions in app
        /// </summary>
        void ConfigureRegions();

        /// <summary>
        ///     method initializes shell window
        /// </summary>
        /// <param name="shell">shell window</param>
        void InitializeShell(DependencyObject shell, IRegionManager rm);

        /// <summary>
        ///     method registers mapping between region adapter and given type.
        /// </summary>
        /// <param name="controlType">type associated with region adapter</param>
        /// <param name="regionAdapter">adapter used to register with type</param>
        void RegisterMapping(Type controlType, IRegionAdapter regionAdapter);
    }
}