using V6Soft.Models.Accounting.ViewModels.MaterialPriceGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.MaterialPriceGroup.Dealers
{
    /// <summary>
    ///     Acts as a service client to get materialPriceGroup data from MaterialPriceGroupService.
    /// </summary>
    public interface IMaterialPriceGroupDataDealer
    {
        /// <summary>
        ///     Gets list of materialPriceGroups satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<MaterialPriceGroupListItem> GetMaterialPriceGroups(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new materialPriceGroup.
        /// </summary>
        bool AddMaterialPriceGroup(AccModels.MaterialPriceGroup materialPriceGroup);
        /// <summary>
        ///     Delete a materialPriceGroup.
        /// </summary>
        bool DeleteMaterialPriceGroup(string key);
        /// <summary>
        ///     Update data for a materialPriceGroup.
        /// </summary>
        bool UpdateMaterialPriceGroup(AccModels.MaterialPriceGroup materialPriceGroup);
    }
}
