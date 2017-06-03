using V6Soft.Models.Accounting.ViewModels.District;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.District.Dealers
{
    /// <summary>
    ///     Acts as a service client to get district data from DistrictService.
    /// </summary>
    public interface IDistrictDataDealer
    {
        /// <summary>
        ///     Gets list of districts satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<DistrictListItem> GetDistricts(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new district.
        /// </summary>
        bool AddDistrict(AccModels.District district);
        /// <summary>
        ///     Delete a district.
        /// </summary>
        bool DeleteDistrict(string key);
        /// <summary>
        ///     Update data for a district.
        /// </summary>
        bool UpdateDistrict(AccModels.District district);
    }
}
