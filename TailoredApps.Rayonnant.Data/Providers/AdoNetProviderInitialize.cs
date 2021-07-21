
using System.Collections.Generic;
using TailoredApps.Rayonnant.Data.Enum;
using TailoredApps.Rayonnant.Interface.DependencyInjection;

namespace TailoredApps.Rayonnant.Data.Providers
{
    public abstract class AdoNetProviderInitialize : IInitialize
    {
        protected readonly IContainer container;

        public AdoNetProviderInitialize(IContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            var conf = new AdoNetDbPersister();

            BuildMappings(conf);
            PostProcessConfiguration(conf);

            container.RegisterInstance(conf);
        }

        protected abstract void BuildMappings(AdoNetDbPersister conf);

        protected virtual void PostProcessConfiguration(AdoNetDbPersister cfg)
        {
        }
    }
}