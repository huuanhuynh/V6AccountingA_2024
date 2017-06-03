using V6Soft.Models.Accounting.ViewModels.Location;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Location.Dealers
{
    /// <summary>
    ///     Acts as a service client to get location data from LocationService.
    /// </summary>
    public interface ILocationDataDealer
    {
        /// <summary>
        ///     Gets list of locations satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<LocationListItem> GetLocations(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new location.
        /// </summary>
        bool AddLocation(AccModels.Location location);
        /// <summary>
        ///     Delete a location.
        /// </summary>
        bool DeleteLocation(string key);
        /// <summary>
        ///     Update data for a location.
        /// </summary>
        bool UpdateLocation(AccModels.Location location);
    }
}
