using V6Soft.Models.Accounting.ViewModels.BankAccount;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Banking.Dealers
{
    /// <summary>
    ///     Acts as a service client to get bankAccount data from BankAccountService.
    /// </summary>
    public interface IBankAccountDataDealer
    {
        /// <summary>
        ///     Gets list of bankAccounts satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<BankAccountListItem> GetBankAccounts(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new bankAccount.
        /// </summary>
        bool AddBankAccount(Models.Accounting.DTO.BankAccount bankAccount);
        /// <summary>
        ///     Delete a bankAccount.
        /// </summary>
        bool DeleteBankAccount(string key);
        /// <summary>
        ///     Update data for a bankAccount.
        /// </summary>
        bool UpdateBankAccount(Models.Accounting.DTO.BankAccount bankAccount);
    }
}
