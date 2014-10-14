using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using Wise.Framework.Interface.DependencyInjection;
using nh = NHibernate;
namespace Wise.Framework.Data.NHibernate
{
    public class NHibernateDataProviderInitialize : IInitialize
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
            container.RegisterInstance<nh.ISessionFactory>(conf.BuildSessionFactory());
        }


        protected virtual Configuration BuildConfiguration()
        {
            return new Configuration();
        }

        protected virtual void BuildMappings(Configuration conf)
        {

        }
        protected virtual void PostProcessConfiguration(Configuration cfg)
        {

        }
    }
}
