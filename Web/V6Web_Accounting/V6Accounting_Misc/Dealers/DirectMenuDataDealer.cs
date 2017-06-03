using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using V6Soft.Accounting.Misc.Farmers;
using V6Soft.Accounting.Misc.Dealers;
using V6Soft.Common.Utils;
using V6Soft.Common.Utils.TaskExtensions;
using V6Soft.Common.Logging;
using V6Soft.Models.Core.ViewModels;


namespace V6Soft.Accounting.Misc.Dealers
{
    /// <summary>
    ///     Implements <see cref="IMenuDataDealer"/>
    /// </summary>
    public class DirectMenuDataDealer : IMenuDataDealer
    {
        private ILogger m_Logger;
        private IMenuDataFarmer m_MenuFarmer;


        /// <summary>
        ///     Initializes an instance of DirectMenuDataDealer
        /// </summary>
        public DirectMenuDataDealer(ILogger logger, IMenuDataFarmer menuFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(menuFarmer, "menuFarmer");

            m_Logger = logger;
            m_MenuFarmer = menuFarmer;
        }

        /// <summary>
        ///     See <see cref="IMenuDataDealer.GetMenuTree"/>
        /// </summary>
        public Task<IList<MenuItem>> GetMenuTree(byte level)
        {
            var source = new TaskCompletionSource<IList<MenuItem>>();

            var task = Task.Factory
                .StartNew(() =>
                {
                    return m_MenuFarmer.GetMenuItems();
                });

            task
                .Then((DisposableEnumerable<MenuItem> disposableItems) =>
                {
                    List<MenuItem> menuItems = disposableItems.ToList();
                    disposableItems.Dispose();

                    if (!menuItems.Any()) {
                        source.SetResult(null);
                        return; 
                    }

                    List<MenuItem> menuTree = BuildMenuTree(menuItems, level);

                    source.SetResult(menuTree);
                })
                .Catch(ex =>
                {
                    source.SetException(ex.InnerException);
                });

            return source.Task;
        }

        /// <summary>
        ///     See <see cref="IMenuDataDealer.GetChildren"/>
        /// </summary>
        public Task<IList<MenuItem>> GetChildren(int oid)
        {
            var source = new TaskCompletionSource<IList<MenuItem>>();

            var task = Task.Factory
                .StartNew(() =>
                {
                    return m_MenuFarmer.GetChildren(oid);
                });

            task
                .Then((DisposableEnumerable<MenuItem> disposableItems) =>
                {
                    List<MenuItem> menuItems = disposableItems.ToList();
                    disposableItems.Dispose();

                    if (!menuItems.Any())
                    {
                        source.SetResult(null);
                        return;
                    }

                    source.SetResult(menuItems);
                })
                .Catch(ex =>
                {
                    source.SetException(ex.InnerException);
                });

            return source.Task;
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
