using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using V6Soft.Models.Core;
using V6Soft.Services.Accounting.Interfaces;
using V6Soft.Common.Utils;


namespace V6Soft.Services.Accounting.DataBakers
{
    /// <summary>
    ///     Implements <see cref="IMenuDataBaker"/>
    /// </summary>
    public class MenuDataBaker : IMenuDataBaker
    {
        private IMenuDataFarmer m_MenuFarmer;

        /// <summary>
        ///     Initializes a new instance of MenuDataBaker class
        ///     with specified <paramref name="menuFarmer"/>
        /// </summary>
        /// <param name="menuFarmer"></param>
        public MenuDataBaker(IMenuDataFarmer menuFarmer)
        {
            m_MenuFarmer = menuFarmer;
        }

        /// <summary>
        ///     See <see cref="IMenuDataBaker.GetMenuItems"/>
        /// </summary>
        public IList<MenuItem> GetMenuTree(byte level = 0)
        {
            List<MenuItem> menuItems;
            
            using (DisposableEnumerable<MenuItem> disposableItems =
                m_MenuFarmer.GetMenuItems())
            {
                menuItems = disposableItems.ToList();
                if (!menuItems.Any()) { return null; }
            }

            List<MenuItem> menuTree = BuildMenuTree(menuItems, level);
            return menuTree;
        }
        
        /// <summary>
        ///     See <see cref="IMenuDataBaker.GetChildren"/>
        /// </summary>
        public IList<MenuItem> GetChildren(int oid)
        {
            
            using (DisposableEnumerable<MenuItem> disposableItems =
                m_MenuFarmer.GetChildren(oid))
            {
                List<MenuItem> menuItems;
                menuItems = disposableItems.ToList();

                if (!menuItems.Any()) { return null; }

                return menuItems;
            }
        }


        private List<MenuItem> BuildMenuTree(List<MenuItem> menuItems, byte level)
        {
            var menuTree = new List<MenuItem>();
            var menuDic = new Dictionary<int, MenuItem>();
            MenuItem currentParent = null;

            foreach (var item in menuItems)
            {
                if (!item.ParentOID.HasValue)
                {
                    // The result menu list only contains 1st-level menu items.
                    menuTree.Add(item);
                    menuDic.Add(item.OID, item);
                    item.Level = 1;
                    continue;
                }

                if (currentParent == null)
                {
                    currentParent = SwitchCurrentParent(menuDic, item);
                }
                else if (currentParent.OID != item.ParentOID)
                {
                    currentParent = SwitchCurrentParent(menuDic, item);
                }

                // Returning `false` means we have had enough levels
                // we need.
                if (!AdoptChild(menuDic, currentParent, item, level))
                {
                    currentParent.Descendants = null;
                    break;
                }
            }
            return menuTree;
        }        
        
        private MenuItem SwitchCurrentParent(Dictionary<int, MenuItem> menuDic, 
            MenuItem item)
        {
            MenuItem currentParent = menuDic[item.ParentOID.Value];
            
            // Creates new list if there is not any.
            currentParent.Descendants = currentParent.Descendants ??
                new List<MenuItem>();
            currentParent.HasDescendants = true;
            return currentParent;
        }

        private bool AdoptChild(Dictionary<int, MenuItem> menuDic, 
            MenuItem currentParent, MenuItem item, byte level)
        {
            if (currentParent.Level == level) { return false; }

            item.Level = (byte)(currentParent.Level + 1);
            item.Parent = currentParent;
            currentParent.Descendants.Add(item);
            menuDic.Add(item.OID, item);
            return true;
        }

    }
}
