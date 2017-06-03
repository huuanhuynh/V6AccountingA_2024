using V6Soft.Models.Accounting.ViewModels.ForeignExchangeRate;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Currency.Dealers
{
    /// <summary>
    ///     Acts as a service client to get foreignExchangeRate data from ForeignExchangeRateService.
    /// </summary>
    public interface IForeignExchangeRateDataDealer
    {
        /// <summary>
        ///     Gets list of foreignExchangeRates satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<ForeignExchangeRateListItem> GetForeignExchangeRates(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new foreignExchangeRate.
        /// </summary>
        bool AddForeignExchangeRate(Models.Accounting.DTO.ForeignExchangeRate foreignExchangeRate);
        /// <summary>
        ///     Delete a foreignExchangeRate.
        /// </summary>
        bool DeleteForeignExchangeRate(string key);
        /// <summary>
        ///     Update data for a foreignExchangeRate.
        /// </summary>
        bool UpdateForeignExchangeRate(Models.Accounting.DTO.ForeignExchangeRate foreignExchangeRate);
    }
}
