using V6Soft.Accounting.Common.Farmers;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Banking.Farmers
{
    /// <summary>
    ///     Provides API for TaiKhoanNganHang data farmer.
    /// </summary>
    public interface IBankAccountDataFarmer : IDataFarmerBase<AccModels.BankAccount>
    {
        /// <summary>
        ///     Gets list of TaiKhoanNganHangs satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<AccModels.BankAccount> GetBankAccounts(SearchCriteria criteria);
    }
}
