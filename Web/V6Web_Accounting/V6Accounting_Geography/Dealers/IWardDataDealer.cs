using V6Soft.Models.Accounting.ViewModels.Ward;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Geography.Dealers
{
    /// <summary>
    ///     Acts as a service client to get ward data from WardService.
    /// </summary>
    public interface IWardDataDealer
    {
        /// <summary>
        ///     Gets list of wards satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<WardListItem> GetWards(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new ward.
        /// </summary>
        bool AddWard(Models.Accounting.DTO.Ward ward);
        /// <summary>
        ///     Delete a ward.
        /// </summary>
        bool DeleteWard(string key);
        /// <summary>
        ///     Update data for a ward.
        /// </summary>
        bool UpdateWard(Models.Accounting.DTO.Ward ward);
    }
}
