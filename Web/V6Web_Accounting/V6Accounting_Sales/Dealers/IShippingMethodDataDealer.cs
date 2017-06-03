using V6Soft.Models.Accounting.ViewModels.ShippingMethod;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.ShippingMethod.Dealers
{
    /// <summary>
    ///     Acts as a service client to get shippingMethod data from ShippingMethodService.
    /// </summary>
    public interface IShippingMethodDataDealer
    {
        /// <summary>
        ///     Gets list of shippingMethods satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<ShippingMethodListItem> GetShippingMethods(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new shippingMethod.
        /// </summary>
        bool AddShippingMethod(AccModels.ShippingMethod shippingMethod);
        /// <summary>
        ///     Delete a shippingMethod.
        /// </summary>
        bool DeleteShippingMethod(string key);
        /// <summary>
        ///     Update data for a shippingMethod.
        /// </summary>
        bool UpdateShippingMethod(AccModels.ShippingMethod shippingMethod);
    }
}
