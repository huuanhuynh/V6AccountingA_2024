using V6Soft.Models.Accounting.ViewModels.Nation;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Nation.Dealers
{
    /// <summary>
    ///     Acts as a service client to get nation data from NationService.
    /// </summary>
    public interface INationDataDealer
    {
        /// <summary>
        ///     Gets list of nations satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<NationListItem> GetNations(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new nation.
        /// </summary>
        bool AddNation(AccModels.Nation nation);
        /// <summary>
        ///     Delete a nation.
        /// </summary>
        bool DeleteNation(string key);
        /// <summary>
        ///     Update data for a nation.
        /// </summary>
        bool UpdateNation(AccModels.Nation nation);
    }
}
