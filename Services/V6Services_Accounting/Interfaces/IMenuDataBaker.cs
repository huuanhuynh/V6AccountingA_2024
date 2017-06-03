using System.Collections.Generic;

using V6Soft.Models.Core;


namespace V6Soft.Services.Accounting.Interfaces
{
    /// <summary>
    ///     Provides methods to operate menu items.
    /// </summary>
    public interface IMenuDataBaker
    {
        /// <summary>
        ///     Gets menu tree including menu items and their chilren. 
        ///     The number of levels of descendants are specified by <paramref name="level" />.
        ///     Pass 0 to get all levels.
        ///     <para/>Returns null if there is no item.
        /// </summary>
        IList<MenuItem> GetMenuTree(byte level = 0);

        /// <summary>
        ///     Gets children of the menu item with specified OID.
        ///     <para/>Returns null if there is no item.
        /// </summary>
        IList<MenuItem> GetChildren(int oid);
    }
}
