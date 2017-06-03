using V6Soft.Models.Accounting.ViewModels.DiscountType;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.DiscountType.Dealers
{
    /// <summary>
    ///     Acts as a service client to get discountType data from DiscountTypeService.
    /// </summary>
    public interface IDiscountTypeDataDealer
    {
        /// <summary>
        ///     Gets list of discountTypes satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<DiscountTypeListItem> GetDiscountTypes(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new discountType.
        /// </summary>
        bool AddDiscountType(AccModels.DiscountType discountType);
        /// <summary>
        ///     Delete a discountType.
        /// </summary>
        bool DeleteDiscountType(string key);
        /// <summary>
        ///     Update data for a discountType.
        /// </summary>
        bool UpdateDiscountType(AccModels.DiscountType discountType);
    }
}
