using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Criterion;
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
            var entity = new MyEntityClass { DummYString = "asd" };
            MyEntityClass readedEntitiy = null;
            using (var session = container.Resolve<SessionScope>())
            {
                var repo = container.Resolve<IRepository>();

                repo.Save(entity);
            }

            using (var session = container.Resolve<SessionScope>())
            {
                var repo = container.Resolve<IRepository>();
                readedEntitiy = repo.GetById<int, MyEntityClass>(1);

                Assert.IsNotNull(readedEntitiy);
                Assert.AreEqual(readedEntitiy.Id, entity.Id);
                Assert.AreEqual(readedEntitiy.DummYString, entity.DummYString);

                var resultset = repo.GetBySearchCriteria(new MyEntityNhibernateSearchCriteria() {DummyString = "asd"});

                Assert.IsNotNull(resultset);

                Assert.IsTrue(resultset.Count()==1);
            
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
                HbmMapping mapping = GetMappings();
                conf.AddDeserializedMapping(mapping, "NHSchemaTest");
                SchemaMetadataUpdater.QuoteTableAndColumns(conf);
            }


            private static HbmMapping GetMappings()
            {
                var mapper = new ModelMapper();

                mapper.AddMappings(Assembly.GetAssembly(typeof(MyEntityClassMapper)).GetExportedTypes());
                HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

                return mapping;
            }

            protected override void PostProcessConfiguration(Configuration cfg)
            {
                new SchemaExport(cfg).Drop(false, true);
                new SchemaExport(cfg).Create(false, true);
            }
        }

        public class MyEntityClass
        {
            public virtual int Id { get; set; }
            public virtual string DummYString { get; set; }
        }


        public class MyEntityNhibernateSearchCriteria : BaseNhibernateSearchCriteria<MyEntityClass>
        {
            public string DummyString { get; set; }

            protected override DetachedCriteria GetCriteria(DetachedCriteria detachedCriteria)
            {
                detachedCriteria.Add(Restrictions.Eq("DummYString", DummyString));
                return detachedCriteria;
            }
        }

        public class MyEntityClassMapper : ClassMapping<MyEntityClass>
        {
            public MyEntityClassMapper()
            {
                Table("MyEntity");

                Id(x => x.Id, mapper => mapper.Generator(Generators.Identity));

                Property(p => p.DummYString, map =>
                {
                    map.Length(255);
                    map.NotNullable(true);
                    map.Lazy(true);
                });
            }
        }
    }
}