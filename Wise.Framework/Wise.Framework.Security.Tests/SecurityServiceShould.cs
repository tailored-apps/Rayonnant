using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Wise.Framework.DependencyInjection.Unity;
using Wise.Framework.Environment;
using Wise.Framework.Interface.Environment;
using Wise.Framework.Interface.Security;
using Wise.Framework.Security.Authentication;
using Wise.Framework.Security.Providers;

namespace Wise.Framework.Security.Tests
{
    public class DummyIdentity : IIdentity
    {
        public string Name { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
    }

    [TestClass]
    public class SecurityServiceShould
    {
        [TestMethod]
        public void TestMethod1()
        {
            UnityContainerAdapter container = new UnityContainerAdapter();
            var envService = new Mock<IEnvironmentService>();
            var secProvider = new Mock<BaseSecurityProvider>();

            secProvider.Setup(x => x.GetCurrentIddentity())
                .Returns(
                    () =>
                        new DummyIdentity()
                        {
                            Name = "Lukasz Kowalski",
                            AuthenticationType = "Local",
                            IsAuthenticated = true
                        });

            secProvider.Setup(x => x.GetRoles(It.IsAny<IIdentity>()))
                .Returns(() => new List<IRole>()
                {
                    Role.Create(@"BUILTIN\Users"),
                    Role.Create(@"BUILTIN\Admin"),
                    Role.Create(@"Everybody")
                });

            secProvider.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns<string>((role) => secProvider.Object.GetRoles(new DummyIdentity()).Any(x => x.Name == role));

            container.RegisterInstance(secProvider.Object);

            envService.Setup(x => x.GetEnvironmentInfo()).Returns(() => new EnvironmentInfo()
            {
                Address = "Asd",
                SelfContained = false,
                Code = "asd",
                HostName = "localhost"
            });

            ISecurityService securityService = new SecurityService(envService.Object, container);
            Assert.IsNotNull(securityService.User);
            var roles = securityService.GetUserRoles(securityService.User).ToList();
            Assert.IsNotNull(roles);
            Assert.IsTrue(securityService.IsInRole(@"BUILTIN\Users"));
        }
    }
}
