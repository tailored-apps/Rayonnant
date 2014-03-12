using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Wise.Framework.Data.Interface;
using Wise.Framework.Interface.Data;

namespace Wise.Framework.Data.Tests
{
    [TestClass]
    public class RepositoryTest : TestBase
    {
        //private readonly Mock<IDataProvider> repositroyMoq = new Mock<IDataProvider>();


        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    //TestContext.Properties["container"] = new UnityContainer();
        //    //container = TestContext.Properties["container"] as IUnityContainer;
        //    //container.RegisterType<IRepository, Repository>();
        //    //container.RegisterInstance<IDataProvider>(repositroyMoq.Object);
        //}

        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    //repositroyMoq.ResetCalls();
        //}
        
        [TestMethod]
        public void GetTest()
        {
            //var repo = container.Resolve<IRepository>();
            //repo.Get<object>(2);
            //repositroyMoq.Verify(x => x.Get<object>(1), Times.Never);
            //repositroyMoq.Verify(x => x.Get<object>(2), Times.Once);
        }
        [TestMethod]
        public void GetByIdTest()
        {
            //var repo = container.Resolve<IRepository>();
            //repo.GetById<long,object>(2);
            //repositroyMoq.Verify(x => x.GetById<long, object>(1), Times.Never);
            //repositroyMoq.Verify(x => x.GetById<long, object>(2), Times.Once);
        }
    }
}
