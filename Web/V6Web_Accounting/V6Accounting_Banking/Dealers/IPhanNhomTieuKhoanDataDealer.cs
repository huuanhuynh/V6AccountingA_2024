using V6Soft.Models.Accounting.ViewModels.PhanNhomTieuKhoan;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Banking.Dealers
{
    /// <summary>
    ///     Acts as a service client to get phanNhomTieuKhoan data from PhanNhomTieuKhoanService.
    /// </summary>
    public interface IPhanNhomTieuKhoanDataDealer
    {
        /// <summary>
        ///     Gets list of phanNhomTieuKhoans satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<PhanNhomTieuKhoanItem> GetPhanNhomTieuKhoans(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new phanNhomTieuKhoan.
        /// </summary>
        bool AddPhanNhomTieuKhoan(Models.Accounting.DTO.PhanNhomTieuKhoan phanNhomTieuKhoan);
        /// <summary>
        ///     Delete a phanNhomTieuKhoan.
        /// </summary>
        bool DeletePhanNhomTieuKhoan(string key);
        /// <summary>
        ///     Update data for a phanNhomTieuKhoan.
        /// </summary>
        bool UpdatePhanNhomTieuKhoan(Models.Accounting.DTO.PhanNhomTieuKhoan phanNhomTieuKhoan);
    }
}
