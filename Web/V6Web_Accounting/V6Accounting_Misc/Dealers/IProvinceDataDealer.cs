using V6Soft.Models.Accounting.ViewModels.Province;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Misc.Dealers
{
    /// <summary>
    ///     Acts as a service client to get province data from ProvinceService.
    /// </summary>
    public interface IProvinceDataDealer
    {
        /// <summary>
        ///     Gets list of provinces satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<ProvinceListItem> GetProvinces(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new province.
        /// </summary>
        bool AddProvince(Models.Accounting.DTO.Province province);
        /// <summary>
        ///     Delete a province.
        /// </summary>
        bool DeleteProvince(string key);
        /// <summary>
        ///     Update data for a province.
        /// </summary>
        bool UpdateProvince(Models.Accounting.DTO.Province province);
    }
}
