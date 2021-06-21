using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using CommonServiceLocator;
using Prism.Regions;
using Prism.Regions.Behaviors;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;
using Wise.Framework.Presentation.Interface.Modularity;

namespace Wise.Framework.Presentation.Modularity
{
    /// <summary>
    ///     Default impl of <see cref="IRegionConfigurator" />
    /// </summary>
    public class RegionConfigurator : IRegionConfigurator
    {
        /// <summary>
        ///     <see cref="IRegionConfigurator.ConfigureContainer" />
        /// </summary>
        public void ConfigureContainer(IContainer container)
        {
            ServiceLocator.SetLocatorProvider(() => container.Resolve<IServiceLocator>());
            container.RegisterTypeIfMissing<RegionAdapterMappings, RegionAdapterMappings>(LifetimeScope.Singleton);
            container.RegisterTypeIfMissing<IRegionManager, RegionManager>(LifetimeScope.Singleton);
            container.RegisterTypeIfMissing<IRegionViewRegistry, RegionViewRegistry>(LifetimeScope.Singleton);
            container.RegisterTypeIfMissing<IRegionBehaviorFactory, RegionBehaviorFactory>(LifetimeScope.Singleton);
            container.RegisterTypeIfMissing<IRegionNavigationJournalEntry, RegionNavigationJournalEntry>(
                LifetimeScope.Singleton);
            container.RegisterTypeIfMissing<IRegionNavigationJournal, RegionNavigationJournal>(LifetimeScope.Singleton);
            container.RegisterTypeIfMissing<IRegionNavigationService, RegionNavigationService>(LifetimeScope.Singleton);
            container.RegisterTypeIfMissing<IRegionNavigationContentLoader, RegionNavigationContentLoader>(
                LifetimeScope.Singleton);
        }

        /// <summary>
        ///     <see cref="IRegionConfigurator.ConfigureRegions" />
        /// </summary>
        public void ConfigureRegions()
        {
            ConfigureRegionAdapterMappings();
            ConfigureDefaultRegionBehaviours();
        }

        /// <summary>
        ///     <see cref="IRegionConfigurator.InitializeShell" />
        /// </summary>
        public void InitializeShell(DependencyObject shell, IRegionManager rm)
        {
            //RegionManager.UpdateRegions();
            RegionManager.SetRegionManager(shell, rm);
        }

        /// <summary>
        ///     <see cref="IRegionConfigurator.RegisterMapping" />
        /// </summary>
        public void RegisterMapping(Type controlType, IRegionAdapter regionAdapter)
        {
            var regionAdapterMApping = ServiceLocator.Current.GetInstance<RegionAdapterMappings>();
            if (regionAdapterMApping != null)
            {
                regionAdapterMApping.RegisterMapping(controlType, regionAdapter);
            }
        }

        /// <summary>
        ///     helper method used to register default regions behaviors
        /// </summary>
        private void ConfigureDefaultRegionBehaviours()
        {
            var defaultBehaviourDict = ServiceLocator.Current.GetInstance<IRegionBehaviorFactory>();
            if (defaultBehaviourDict != null)
            {
                defaultBehaviourDict.AddIfMissing(AutoPopulateRegionBehavior.BehaviorKey,
                    typeof (AutoPopulateRegionBehavior));
                defaultBehaviourDict.AddIfMissing(BindRegionContextToDependencyObjectBehavior.BehaviorKey,
                    typeof (BindRegionContextToDependencyObjectBehavior));
                defaultBehaviourDict.AddIfMissing(RegionActiveAwareBehavior.BehaviorKey,
                    typeof (RegionActiveAwareBehavior));
                defaultBehaviourDict.AddIfMissing(SyncRegionContextWithHostBehavior.BehaviorKey,
                    typeof (SyncRegionContextWithHostBehavior));
                defaultBehaviourDict.AddIfMissing(RegionManagerRegistrationBehavior.BehaviorKey,
                    typeof (RegionManagerRegistrationBehavior));
                defaultBehaviourDict.AddIfMissing(RegionMemberLifetimeBehavior.BehaviorKey,
                    typeof (RegionMemberLifetimeBehavior));
            }
        }

        /// <summary>
        ///     helper method used to Configure Region Adapter Mappings
        /// </summary>
        private void ConfigureRegionAdapterMappings()
        {
            var regionAdapterMappings = ServiceLocator.Current.GetInstance<RegionAdapterMappings>();
            if (regionAdapterMappings != null)
            {
                regionAdapterMappings.RegisterMapping(typeof (Selector),
                    ServiceLocator.Current.GetInstance<SelectorRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof (ItemsControl),
                    ServiceLocator.Current.GetInstance<ItemsControlRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof (ContentControl),
                    ServiceLocator.Current.GetInstance<ContentControlRegionAdapter>());
            }
        }
    }
}