using V6Soft.Models.Accounting.ViewModels.Account;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Banking.Dealers
{
    /// <summary>
    ///     Acts as a service client to get taiKhoan data from AccountService.
    /// </summary>
    public interface IAccountDataDealer
    {
        /// <summary>
        ///     Gets list of taiKhoans satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<AccountListItem> GetAccounts(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new taiKhoan.
        /// </summary>
        bool AddAccount(Models.Accounting.DTO.TaiKhoan taiKhoan);
        /// <summary>
        ///     Delete a taiKhoan.
        /// </summary>
        bool DeleteAccount(string key);
        /// <summary>
        ///     Update data for a taiKhoan.
        /// </summary>
        bool UpdateAccount(Models.Accounting.DTO.TaiKhoan taiKhoan);
    }
}
