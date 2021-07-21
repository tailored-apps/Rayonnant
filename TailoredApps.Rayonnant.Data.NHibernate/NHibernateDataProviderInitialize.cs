using NHibernate.Cfg;
using TailoredApps.Rayonnant.Interface.DependencyInjection;
using nh = NHibernate;

namespace TailoredApps.Rayonnant.Data.NHibernate
{
    public abstract class NHibernateDataProviderInitialize : IInitialize
    {
        protected readonly IContainer container;

        public NHibernateDataProviderInitialize(IContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            Configuration conf = BuildConfiguration();

            BuildMappings(conf);
            PostProcessConfiguration(conf);
            conf.Configure();
            container.RegisterInstance(conf.BuildSessionFactory());
        }

        protected virtual Configuration BuildConfiguration()
        {
            return new Configuration();
        }

        protected abstract void BuildMappings(Configuration conf);

        protected virtual void PostProcessConfiguration(Configuration cfg)
        {
        }
    }
}