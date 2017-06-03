using V6Soft.Models.Accounting.ViewModels.ServiceStatus;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.ServiceStatus.Dealers
{
    /// <summary>
    ///     Acts as a service client to get serviceStatus data from ServiceStatusService.
    /// </summary>
    public interface IServiceStatusDataDealer
    {
        /// <summary>
        ///     Gets list of serviceStatuss satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<ServiceStatusListItem> GetServiceStatuss(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new serviceStatus.
        /// </summary>
        bool AddServiceStatus(AccModels.ServiceStatus serviceStatus);
        /// <summary>
        ///     Delete a serviceStatus.
        /// </summary>
        bool DeleteServiceStatus(string key);
        /// <summary>
        ///     Update data for a serviceStatus.
        /// </summary>
        bool UpdateServiceStatus(AccModels.ServiceStatus serviceStatus);
    }
}
