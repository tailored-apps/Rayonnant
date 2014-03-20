using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Tool.hbm2ddl;
using Wise.Framework.Data.Interface;
using Wise.Framework.Data.NHibernate;
using Wise.Framework.DependencyInjection.Unity;
using Wise.Framework.Interface.Data;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;

namespace Wise.Framework.Data.Tests.Integration
{
    [TestClass]
    public class RepositoryIntegrationTest : TestBase
    {


        [TestInitialize]
        public void TestInitialize()
        {
            TestContext.Properties["container"] = new UnityContainerAdapter();
            container = TestContext.Properties["container"] as IContainer;
            container.RegisterTypeIfMissing<IInitialize, CustomInintializer>(LifetimeScope.Singleton);
            container.RegisterTypeIfMissing<IDataProvider, NHibernateDataProvider>(LifetimeScope.Singleton);
            container.RegisterTypeIfMissing<IRepository, Repository>(LifetimeScope.Singleton);
            var elements = container.Resolve<CustomInintializer>();





            elements.Initialize();

        }
        [TestMethod]
        public void TestMethod1()
        {
            using (var session = container.Resolve<SessionScope>())
            {
                var repo = container.Resolve<IRepository>();
                var entity = repo.GetById<int, MyEntityClass>(1);
                    
            }
        }

        public class MyEntityClass
        {
            public virtual int Id { get; set; }
            public virtual string DummYString { get; set; }
        }




        public class MyEntityClassMapper : ClassMapping<MyEntityClass>
        {
            public MyEntityClassMapper()
            {
                Table("MyEntity");
                Id(x => x.Id);
                Property(p => p.DummYString, map =>
                {
                    map.Length(3);
                    map.NotNullable(true);
                });

            }
        }

        public class CustomInintializer : NHibernateDataProviderInitialize
        {
            public CustomInintializer(IContainer container)
                : base(container)
            {
            }

            protected override void BuildMappings(Configuration conf)
            {
                
                var mapping = GetMappings();
                conf.AddDeserializedMapping(mapping, "NHSchemaTest");
                SchemaMetadataUpdater.QuoteTableAndColumns(conf);

            }


            private static HbmMapping GetMappings()
            {
                var mapper = new ModelMapper();

                mapper.AddMappings(Assembly.GetAssembly(typeof(MyEntityClassMapper)).GetExportedTypes());
                var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

                return mapping;
            }

            protected override void PostProcessConfiguration(Configuration cfg)
            {
                new SchemaExport(cfg).Create(false, true);
             
            }
        }
    }
}
