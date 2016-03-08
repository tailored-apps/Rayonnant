
using System.Collections.Generic;
using Wise.Framework.Data.Enum;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.Data.Providers
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