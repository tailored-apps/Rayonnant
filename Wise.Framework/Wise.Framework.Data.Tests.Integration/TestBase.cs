using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.Data.Tests.Integration
{
    public class TestBase
    {
        protected IContainer container;

        private static TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

    }
}
