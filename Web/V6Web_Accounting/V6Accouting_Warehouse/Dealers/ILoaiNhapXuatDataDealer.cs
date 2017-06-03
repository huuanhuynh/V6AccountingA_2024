using V6Soft.Models.Accounting.ViewModels.LoaiNhapXuat;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.LoaiNhapXuat.Dealers
{
    /// <summary>
    ///     Acts as a service client to get loaiNhapXuat data from LoaiNhapXuatService.
    /// </summary>
    public interface ILoaiNhapXuatDataDealer
    {
        /// <summary>
        ///     Gets list of loaiNhapXuats satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<LoaiNhapXuatItem> GetLoaiNhapXuats(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new loaiNhapXuat.
        /// </summary>
        bool AddLoaiNhapXuat(AccModels.LoaiNhapXuat loaiNhapXuat);
        /// <summary>
        ///     Delete a loaiNhapXuat.
        /// </summary>
        bool DeleteLoaiNhapXuat(string key);
        /// <summary>
        ///     Update data for a loaiNhapXuat.
        /// </summary>
        bool UpdateLoaiNhapXuat(AccModels.LoaiNhapXuat loaiNhapXuat);
    }
}
