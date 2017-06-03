using System.Collections.Generic;

using V6Soft.Common.Utils;
using V6Soft.Models.Core.ViewModels;


namespace V6Soft.Accounting.Misc.Farmers
{
    public interface IMenuDataFarmer
    {
        /// <summary>
        ///     Gets menu tree including menu items and their chilren.
        ///     <para/>Returns well-fed or empty collection if there is no item.
        ///     Never returns null.
        /// </summary>
        DisposableEnumerable<MenuItem> GetMenuItems();

        /// <summary>
        ///     Gets children of the menu item with specified OID.
        ///     <para/>Returns well-fed or empty collection if there is no item.
        ///     Never returns null.
        /// </summary>
        DisposableEnumerable<MenuItem> GetChildren(int oid);
    }
}
