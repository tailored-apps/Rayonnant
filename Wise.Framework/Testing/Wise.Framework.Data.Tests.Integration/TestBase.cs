using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.Data.Tests.Integration
{
    public class TestBase
    {
        private static TestContext testContextInstance;
        protected IContainer container;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
    }
}