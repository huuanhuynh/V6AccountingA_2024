using V6Soft.Models.Accounting.ViewModels.Tax;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Tax.Dealers
{
    /// <summary>
    ///     Acts as a service client to get tax data from TaxService.
    /// </summary>
    public interface ITaxDataDealer
    {
        /// <summary>
        ///     Gets list of taxs satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<TaxListItem> GetTaxs(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new tax.
        /// </summary>
        bool AddTax(AccModels.Tax tax);
        /// <summary>
        ///     Delete a tax.
        /// </summary>
        bool DeleteTax(string key);
        /// <summary>
        ///     Update data for a tax.
        /// </summary>
        bool UpdateTax(AccModels.Tax tax);
    }
}
