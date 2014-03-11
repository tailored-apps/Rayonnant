using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Wise.Framework.Data.Tests
{
    public class TestBase
    {
        protected IUnityContainer container;

        private static TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

    }
}
