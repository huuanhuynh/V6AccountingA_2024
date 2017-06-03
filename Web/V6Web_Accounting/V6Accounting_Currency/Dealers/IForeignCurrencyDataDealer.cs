using V6Soft.Models.Accounting.ViewModels.ForeignCurrency;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.ForeignCurrency.Dealers
{
    /// <summary>
    ///     Acts as a service client to get foreignCurrency data from ForeignCurrencyService.
    /// </summary>
    public interface IForeignCurrencyDataDealer
    {
        /// <summary>
        ///     Gets list of foreignCurrencys satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<ForeignCurrencyListItem> GetForeignCurrencys(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new foreignCurrency.
        /// </summary>
        bool AddForeignCurrency(AccModels.ForeignCurrency foreignCurrency);
        /// <summary>
        ///     Delete a foreignCurrency.
        /// </summary>
        bool DeleteForeignCurrency(string key);
        /// <summary>
        ///     Update data for a foreignCurrency.
        /// </summary>
        bool UpdateForeignCurrency(AccModels.ForeignCurrency foreignCurrency);
    }
}
