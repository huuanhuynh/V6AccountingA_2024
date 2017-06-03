using V6Soft.Models.Accounting.ViewModels.DanhMucLoHang;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Merchandise.Dealers
{
    /// <summary>
    ///     Acts as a service client to get merchandise data from MerchandiseService.
    /// </summary>
    public interface IMerchandiseDataDealer
    {
        /// <summary>
        ///     Gets list of merchandises satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<MerchandiseListItem> GetMerchandises(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new merchandise.
        /// </summary>
        bool AddMerchandise(AccModels.Merchandise merchandise);
        /// <summary>
        ///     Delete a merchandise.
        /// </summary>
        bool DeleteMerchandise(string key);
        /// <summary>
        ///     Update data for a merchandise.
        /// </summary>
        bool UpdateMerchandise(AccModels.Merchandise merchandise);
    }
}
