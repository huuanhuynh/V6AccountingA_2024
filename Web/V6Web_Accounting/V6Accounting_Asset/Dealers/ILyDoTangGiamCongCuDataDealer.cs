using V6Soft.Models.Accounting.ViewModels.EquipmentChangedReason;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Asset.Dealers
{
    /// <summary>
    ///     Acts as a service client to get customer data from LyDoTangGiamCongCuService.
    /// </summary>
    public interface ILyDoTangGiamCongCuDataDealer
    {
        /// <summary>
        ///     Gets list of customers satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<EquipmentChangedReasonListItem> GetLyDoTangGiamCongCus(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new customer.
        /// </summary>
        bool AddLyDoTangGiamCongCu(AccModels.LyDoTangGiamCongCu customer);
        /// <summary>
        ///     Delete a customer.
        /// </summary>
        bool DeleteLyDoTangGiamCongCu(string key);
        /// <summary>
        ///     Update data for a customer.
        /// </summary>
        bool UpdateLyDoTangGiamCongCu(AccModels.LyDoTangGiamCongCu lyDoTangGiamCongCu);
    }
}
