using V6Soft.Models.Accounting.ViewModels.Capital;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Capital.Dealers
{
    /// <summary>
    ///     Acts as a service client to get capital data from CapitalService.
    /// </summary>
    public interface ICapitalDataDealer
    {
        /// <summary>
        ///     Gets list of capitals satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<CapitalListItem> GetCapitals(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new capital.
        /// </summary>
        bool AddCapital(AccModels.Capital capital);
        /// <summary>
        ///     Delete a capital.
        /// </summary>
        bool DeleteCapital(string key);
        /// <summary>
        ///     Update data for a capital.
        /// </summary>
        bool UpdateCapital(AccModels.Capital capital);
    }
}
