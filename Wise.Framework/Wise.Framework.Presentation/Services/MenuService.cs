using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Wise.Framework.Presentation.Interface.Menu;
using Wise.Framework.Presentation.Interface.ViewModel;

namespace Wise.Framework.Presentation.Services
{
    /// <summary>
    ///     Default implementation of <see cref="IMenuService" />
    ///     Class is responsible for adding removing and getting MenuItem object stored in header Commands view model.
    /// </summary>
    public class MenuService : IMenuService
    {
        /// <summary>
        ///     Default Menu Prefix added to every item which will be added as root of path.
        /// </summary>
        public static readonly string MENU_ITEMS_PREFIX = "#MenuItems:";

        private readonly ICommandsViewModel commandsViewModel;

        /// <summary>
        ///     .ctor
        /// </summary>
        /// <param name="commandsViewModel">Commands view model which is registered as default top bar menu container</param>
        public MenuService(ICommandsViewModel commandsViewModel)
        {
            this.commandsViewModel = commandsViewModel;
        }


        /// <summary>
        ///     Method responsible for getting Menu Items on specyfic path.
        ///     Path are stored in format <see cref="MENU_ITEMS_PREFIX">#MenuItems:</see>|MenuItemRootElement|MenuItemChildElement
        /// </summary>
        /// <param name="path">
        ///     Path in format <see cref="MENU_ITEMS_PREFIX">#MenuItems:</see>
        ///     |MenuItemRootElement|MenuItemChildElement, path is keept as UID of menuItem, it should be keept as unique name
        ///     throught all collection of menu items
        /// </param>
        /// <returns>
        ///     Menu item on specyfied path
        ///     <value>null</value>
        ///     for not matched element
        /// </returns>
        public MenuItem GetMenuItem(string path)
        {
            return Find(commandsViewModel.Commands, path);
        }

        public void AddMenuItem(MenuItem menuItem, string path)
        {
            MenuItem parent = Find(commandsViewModel.Commands, path);
            if (parent != null)
            {
                menuItem.Uid = ComposeUidForElement(menuItem.Header.ToString(), parent);
                parent.Items.Add(menuItem);
            }
            else if (string.Equals(MENU_ITEMS_PREFIX, path))
            {
                menuItem.Uid = ComposeUidForElement(menuItem.Header.ToString(), null);
                commandsViewModel.Commands.Add(menuItem);
            }
            else
            {
                var middleString = MENU_ITEMS_PREFIX;
                var elements = path.Replace(MENU_ITEMS_PREFIX, string.Empty).Split('|');
                MenuItem lastItem = null;

                foreach (string element in elements)
                {
                    lastItem = Find(commandsViewModel.Commands, middleString);
                    if (lastItem == null)
                    {
                        var isRequiredToAdd = ComposeUidForElement(element, null);
                        if (Find(commandsViewModel.Commands, isRequiredToAdd) == null)
                        {
                            lastItem = new MenuItem();
                            lastItem.Header = element;
                            lastItem.Uid = ComposeUidForElement(lastItem.Header.ToString(), null);

                            commandsViewModel.Commands.Add(lastItem);
                        }
                    }
                    else
                    {

                        var i = new MenuItem();
                        i.Header = element;
                        i.Uid = ComposeUidForElement(i.Header.ToString(), lastItem);
                        lastItem.Items.Add(i);
                        lastItem = i;
                    }


                    middleString += middleString.Equals(MENU_ITEMS_PREFIX) ? element : "|" + element;
                }
                lastItem.Items.Add(menuItem);
            }
        }

        /// <summary>
        ///     Method responsible for removing items on provided path.
        /// </summary>
        /// <param name="path">path for item</param>
        public void RemoveMenuItem(string path)
        {
            MenuItem menuToRemove = Find(commandsViewModel.Commands, path);

            if (menuToRemove != null)
            {
                int index = path.LastIndexOf('|');

                if (index < 0)
                {
                    commandsViewModel.Commands.Remove(menuToRemove);
                }
                else
                {
                    string parentItemPath = path.Substring(0, index);
                    MenuItem parent = Find(commandsViewModel.Commands, parentItemPath);
                    parent.Items.Remove(menuToRemove);
                }
            }
        }

        /// <summary>
        ///     method composes uids for menu items its also path used for identyfiing element in tree collection
        /// </summary>
        /// <param name="header">element header name</param>
        /// <param name="parent">parent element if applicable</param>
        /// <returns>
        ///     <value>
        ///         <see cref="MENU_ITEMS_PREFIX">#MenuItems:</see><see cref="header">header</see>
        ///     </value>
        ///     for root element when parent is not passed
        ///     <value>
        ///         <see cref="MENU_ITEMS_PREFIX">#MenuItems:</see><see cref="MenuItem.Header">Root name</see>|
        ///         <see cref="header">header</see>
        ///     </value>
        ///     for root element when parent is referenced
        /// </returns>
        private string ComposeUidForElement(string header, MenuItem parent)
        {
            string pre = parent != null
                ? string.Format("{0}", parent.Uid)
                : string.Format(MENU_ITEMS_PREFIX);

            string uidFormat = pre.Equals(MENU_ITEMS_PREFIX)
                ? "{0}{1}"
                : "{0}|{1}";

            return string.Format(uidFormat, pre, header);
        }

        /// <summary>
        ///     helper method responsible for grabbing menuitem with specyfic uid
        /// </summary>
        /// <param name="items">colection item supposed to be searched</param>
        /// <param name="uid">element uid</param>
        /// <returns>first MenuItem in whole collection with specyfic uid</returns>
        private MenuItem Find(IEnumerable<MenuItem> items, string uid)
        {
            MenuItem menuItem = null;
            foreach (MenuItem item in items)
            {
                if (uid.Trim().Equals(item.Uid, StringComparison.OrdinalIgnoreCase))
                {
                    menuItem = item;
                    break;
                }
                MenuItem child = Find(item.Items.Cast<MenuItem>(), uid);
                if (child != null)
                {
                    menuItem = child;
                    break;
                }
            }

            return menuItem;
        }
    }
}