using System.Collections.Generic;
using System.Threading.Tasks;

using V6Soft.Models.Core.ViewModels;


namespace V6Soft.Accounting.Misc.Dealers
{
    /// <summary>
    ///     Acts as a service client to get menu data from MenuService.
    /// </summary>
    public interface IMenuDataDealer
    {
        /// <summary>
        ///     Gets menu tree including menu items and their chilren. 
        ///     The number of levels of descendants are specified by <paramref name="level"/>.
        ///     Pass 0 to get all levels.
        ///     <para/>Returns null if there is no item.
        /// </summary>
        Task<IList<MenuItem>> GetMenuTree(byte level);

        /// <summary>
        ///     Gets children of the menu item with specified OID.
        ///     <para/>Returns null if there is no item.
        /// </summary>
        Task<IList<MenuItem>> GetChildren(int oid);
    }
}
