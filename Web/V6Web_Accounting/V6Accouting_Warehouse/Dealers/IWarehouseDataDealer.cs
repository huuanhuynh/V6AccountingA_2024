using V6Soft.Models.Accounting.ViewModels.Warehouse;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Warehouse.Dealers
{
    /// <summary>
    ///     Acts as a service client to get warehouse data from WarehouseService.
    /// </summary>
    public interface IWarehouseDataDealer
    {
        /// <summary>
        ///     Gets list of warehouses satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<WarehouseListItem> GetWarehouses(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new warehouse.
        /// </summary>
        bool AddWarehouse(AccModels.Warehouse warehouse);
        /// <summary>
        ///     Delete a warehouse.
        /// </summary>
        bool DeleteWarehouse(string key);
        /// <summary>
        ///     Update data for a warehouse.
        /// </summary>
        bool UpdateWarehouse(AccModels.Warehouse warehouse);
    }
}
