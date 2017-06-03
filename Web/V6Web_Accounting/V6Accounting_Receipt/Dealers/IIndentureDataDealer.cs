using V6Soft.Models.Accounting.ViewModels.Indenture;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Indenture.Dealers
{
    /// <summary>
    ///     Acts as a service client to get indenture data from IndentureService.
    /// </summary>
    public interface IIndentureDataDealer
    {
        /// <summary>
        ///     Gets list of indentures satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<IndentureListItem> GetIndentures(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new indenture.
        /// </summary>
        bool AddIndenture(AccModels.Indenture indenture);
        /// <summary>
        ///     Delete a indenture.
        /// </summary>
        bool DeleteIndenture(string key);
        /// <summary>
        ///     Update data for a indenture.
        /// </summary>
        bool UpdateIndenture(AccModels.Indenture indenture);
    }
}
