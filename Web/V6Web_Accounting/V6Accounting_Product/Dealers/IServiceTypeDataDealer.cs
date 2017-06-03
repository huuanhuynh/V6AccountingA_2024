using V6Soft.Models.Accounting.ViewModels.ServiceType;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.ServiceType.Dealers
{
    /// <summary>
    ///     Acts as a service client to get serviceType data from ServiceTypeService.
    /// </summary>
    public interface IServiceTypeDataDealer
    {
        /// <summary>
        ///     Gets list of serviceTypes satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<ServiceTypeListItem> GetServiceTypes(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new serviceType.
        /// </summary>
        bool AddServiceType(AccModels.ServiceType serviceType);
        /// <summary>
        ///     Delete a serviceType.
        /// </summary>
        bool DeleteServiceType(string key);
        /// <summary>
        ///     Update data for a serviceType.
        /// </summary>
        bool UpdateServiceType(AccModels.ServiceType serviceType);
    }
}
