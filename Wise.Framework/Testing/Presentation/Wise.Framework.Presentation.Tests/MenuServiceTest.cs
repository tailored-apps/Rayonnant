using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Wise.Framework.Presentation.Interface.Menu;
using Wise.Framework.Presentation.Interface.ViewModel;
using Wise.Framework.Presentation.Services;

namespace Wise.Framework.Presentation.Tests
{
    [TestClass]
    public class MenuServiceTest
    {
        private readonly Mock<ICommandsViewModel> commandsViewModel = new Mock<ICommandsViewModel>();

        [TestInitialize]
        public void SetupTest()
        {
            commandsViewModel.ResetCalls();
        }

        [TestMethod]
        public void CanFindProperItem()
        {
            var collection = new ObservableCollection<MenuItem>();


            commandsViewModel.SetupProperty(x => x.Commands).SetupGet(x => x.Commands).Returns(collection);

            IMenuService service = new MenuService(commandsViewModel.Object);
            service.AddMenuItem(new MenuItem() { Header = "asd1" }, "one|two");
            MenuItem element = service.GetMenuItem("one|two|asd1");
            Assert.IsNotNull(element);
            Assert.AreEqual("asd1", element.Header);
        }


        [TestMethod]
        public void CanAddOnSpecyficPathNewMenuItem()
        {
            var menuItems = new ObservableCollection<MenuItem>();
            IMenuService service = new MenuService(commandsViewModel.Object);
            commandsViewModel.SetupProperty(x => x.Commands)
                .SetupGet(x => x.Commands)
                .Returns(menuItems);

            var newItem = new MenuItem { Header = "Asd" };

            service.AddMenuItem(newItem, "");
            Assert.IsNotNull(commandsViewModel.Object.Commands);
            Assert.AreEqual(commandsViewModel.Object.Commands.Count, 1);
            Assert.AreEqual(commandsViewModel.Object.Commands.First().Header, "Asd");


            var otherSubItem = new MenuItem { Header = "SubItem" };
            service.AddMenuItem(otherSubItem, "Asd");


            Assert.IsNotNull(commandsViewModel.Object.Commands);
            Assert.AreEqual(commandsViewModel.Object.Commands.Count, 1);
            Assert.AreEqual(commandsViewModel.Object.Commands.First().Header, "Asd");


            Assert.IsNotNull(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")));
            Assert.AreEqual(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")).Items.Count, 1);
            Assert.AreEqual("SubItem", ((MenuItem)(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")).Items[0])).Header);

            var otherSubSubItem = new MenuItem { Header = "SubSubItem" };
            service.AddMenuItem(otherSubSubItem, "Asd|SubItem");

            Assert.IsNotNull(commandsViewModel.Object.Commands);
            Assert.AreEqual(commandsViewModel.Object.Commands.Count, 1);
            Assert.AreEqual(commandsViewModel.Object.Commands.First().Header, "Asd");


            Assert.IsNotNull(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")));
            Assert.AreEqual(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")).Items.Count, 1);
            Assert.AreEqual("SubItem", ((MenuItem)(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")).Items[0])).Header);

            Assert.AreEqual("SubSubItem", ((MenuItem)((MenuItem)(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")).Items[0])).Items[0]).Header);
            Assert.AreEqual("#MenuItems:Asd|SubItem|SubSubItem", ((MenuItem)((MenuItem)(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")).Items[0])).Items[0]).Uid);
        }

        [TestMethod]
        public void CanRemoveOnSpecyficPathMenuItem()
        {
            var items = new ObservableCollection<MenuItem>();
            IMenuService service = new MenuService(commandsViewModel.Object);
            commandsViewModel.SetupProperty(x => x.Commands)
                .SetupGet(x => x.Commands)
                .Returns(items);

            var newItem = new MenuItem { Header = "Asd" };

            service.AddMenuItem(newItem, "");
            Assert.IsNotNull(commandsViewModel.Object.Commands);
            Assert.AreEqual(commandsViewModel.Object.Commands.Count, 1);
            Assert.AreEqual(commandsViewModel.Object.Commands.First().Header, "Asd");


            var otherSubItem = new MenuItem { Header = "SubItem" };
            service.AddMenuItem(otherSubItem, "Asd");


            Assert.IsNotNull(commandsViewModel.Object.Commands);
            Assert.AreEqual(commandsViewModel.Object.Commands.Count, 1);
            Assert.AreEqual(commandsViewModel.Object.Commands.First().Header, "Asd");


            Assert.IsNotNull(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")));
            Assert.AreEqual(1,commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")).Items.Count);
            Assert.AreEqual("SubItem",((MenuItem)(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")).Items[0])).Header);

            var otherSubSubItem = new MenuItem { Header = "SubSubItem" };
            service.AddMenuItem(otherSubSubItem, "Asd|SubItem");

            Assert.IsNotNull(commandsViewModel.Object.Commands);
            Assert.AreEqual(commandsViewModel.Object.Commands.Count, 1);
            Assert.AreEqual(commandsViewModel.Object.Commands.First().Header, "Asd");


            Assert.IsNotNull(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")));
            Assert.AreEqual(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")).Items.Count,1);
            Assert.AreEqual("SubItem",((MenuItem)(commandsViewModel.Object.Commands.FirstOrDefault(x => x.Header.Equals("Asd")).Items[0])).Header);

            Assert.AreEqual("SubSubItem",((MenuItem)((MenuItem)(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")).Items[0])).Items[0]).Header );
            Assert.AreEqual("#MenuItems:Asd|SubItem|SubSubItem",((MenuItem)((MenuItem)(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")).Items[0])).Items[0]).Uid);


            service.RemoveMenuItem("Asd|SubItem");

            Assert.IsNotNull(commandsViewModel.Object.Commands);
            Assert.AreEqual(commandsViewModel.Object.Commands.Count, 1);
            Assert.AreEqual(commandsViewModel.Object.Commands.First().Header, "Asd");

            Assert.IsNotNull(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")));
            Assert.AreEqual(commandsViewModel.Object.Commands.SingleOrDefault(x => x.Header.Equals("Asd")).Items.Count,
                0);


            service.RemoveMenuItem("Asd");

            Assert.IsNotNull(commandsViewModel.Object.Commands);
            Assert.AreEqual(commandsViewModel.Object.Commands.Count, 0);
        }


        private string ComposeUidForElement(string header, MenuItem parent)
        {
            string pre = parent != null ? string.Format("{0}", parent.Uid) : string.Format("#MenuItems:");

            return string.Format("{0}|{1}", pre, header);
        }
    }
}