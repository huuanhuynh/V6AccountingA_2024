using V6Soft.Models.Accounting.ViewModels.AccountType;
using V6Soft.Models.Accounting.ViewModels.AccountType;
using V6Soft.Models.Accounting.ViewModels.PhanLoaiCongCu;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.AccountType.Dealers
{
    /// <summary>
    ///     Acts as a service client to get phanLoaiTaiKhoan data from AccountTypeService.
    /// </summary>
    public interface IAccountTypeDataDealer
    {
        /// <summary>
        ///     Gets list of phanLoaiTaiKhoans satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<AccountTypeListItem> GetAccountTypes(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new phanLoaiTaiKhoan.
        /// </summary>
        bool AddAccountType(AccModels.AccountType phanLoaiTaiKhoan);
        /// <summary>
        ///     Delete a phanLoaiTaiKhoan.
        /// </summary>
        bool DeleteAccountType(string key);
        /// <summary>
        ///     Update data for a phanLoaiTaiKhoan.
        /// </summary>
        bool UpdateAccountType(AccModels.AccountType phanLoaiTaiKhoan);
    }
}
