using V6Soft.Models.Accounting.ViewModels.CustomerPriceGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.CustomerPriceGroup.Dealers
{
    /// <summary>
    ///     Acts as a service client to get customerPriceGroup data from CustomerPriceGroupService.
    /// </summary>
    public interface ICustomerPriceGroupDataDealer
    {
        /// <summary>
        ///     Gets list of customerPriceGroups satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<CustomerPriceGroupListItem> GetCustomerPriceGroups(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new customerPriceGroup.
        /// </summary>
        bool AddCustomerPriceGroup(AccModels.CustomerPriceGroup customerPriceGroup);
        /// <summary>
        ///     Delete a customerPriceGroup.
        /// </summary>
        bool DeleteCustomerPriceGroup(string key);
        /// <summary>
        ///     Update data for a customerPriceGroup.
        /// </summary>
        bool UpdateCustomerPriceGroup(AccModels.CustomerPriceGroup customerPriceGroup);
    }
}
