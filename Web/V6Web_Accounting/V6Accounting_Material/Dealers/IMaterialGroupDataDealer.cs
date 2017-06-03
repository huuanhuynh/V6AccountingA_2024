using V6Soft.Models.Accounting.ViewModels.MaterialGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.MaterialGroup.Dealers
{
    /// <summary>
    ///     Acts as a service client to get materialGroup data from MaterialGroupService.
    /// </summary>
    public interface IMaterialGroupDataDealer
    {
        /// <summary>
        ///     Gets list of materialGroups satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<MaterialGroupListItem> GetMaterialGroups(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new materialGroup.
        /// </summary>
        bool AddMaterialGroup(AccModels.MaterialGroup materialGroup);
        /// <summary>
        ///     Delete a materialGroup.
        /// </summary>
        bool DeleteMaterialGroup(string key);
        /// <summary>
        ///     Update data for a materialGroup.
        /// </summary>
        bool UpdateMaterialGroup(AccModels.MaterialGroup materialGroup);
    }
}
