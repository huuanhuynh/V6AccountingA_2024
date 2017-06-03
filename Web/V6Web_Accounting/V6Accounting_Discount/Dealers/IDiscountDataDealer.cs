using V6Soft.Models.Accounting.ViewModels.Discount;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Discount.Dealers
{
    /// <summary>
    ///     Acts as a service client to get discount data from DiscountService.
    /// </summary>
    public interface IDiscountDataDealer
    {
        /// <summary>
        ///     Gets list of discounts satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<DiscountListItem> GetDiscounts(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new discount.
        /// </summary>
        bool AddDiscount(AccModels.Discount discount);
        /// <summary>
        ///     Delete a discount.
        /// </summary>
        bool DeleteDiscount(string key);
        /// <summary>
        ///     Update data for a discount.
        /// </summary>
        bool UpdateDiscount(AccModels.Discount discount);
    }
}
