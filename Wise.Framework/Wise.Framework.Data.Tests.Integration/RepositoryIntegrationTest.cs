using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
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
            container.RegisterTypeIfMissing<IInitialize, NHibernateDataProviderInitialize>(LifetimeScope.Singleton);
            container.RegisterTypeIfMissing<IDataProvider, NHibernateDataProvider>(LifetimeScope.Singleton);
            container.RegisterTypeIfMissing<IRepository, Repository>(LifetimeScope.Singleton);
            var elements = container.Resolve<NHibernateDataProviderInitialize>();

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
        }
    }
}
