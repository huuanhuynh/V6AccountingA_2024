using V6Soft.Models.Accounting.ViewModels.PhanNhomCongCu;
using V6Soft.Models.Accounting.ViewModels.PhanLoaiCongCu;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.PhanNhomCongCu.Dealers
{
    /// <summary>
    ///     Acts as a service client to get phanNhomCongCu data from PhanNhomCongCuService.
    /// </summary>
    public interface IPhanNhomCongCuDataDealer
    {
        /// <summary>
        ///     Gets list of phanNhomCongCus satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<PhanNhomCongCuItem> GetPhanNhomCongCus(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new phanNhomCongCu.
        /// </summary>
        bool AddPhanNhomCongCu(AccModels.PhanNhomCongCu phanNhomCongCu);
        /// <summary>
        ///     Delete a phanNhomCongCu.
        /// </summary>
        bool DeletePhanNhomCongCu(string key);
        /// <summary>
        ///     Update data for a phanNhomCongCu.
        /// </summary>
        bool UpdatePhanNhomCongCu(AccModels.PhanNhomCongCu phanNhomCongCu);
    }
}
