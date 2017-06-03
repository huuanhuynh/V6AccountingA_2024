using V6Soft.Accounting.Common.Farmers;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.NhomKhachHang.Farmers
{
    /// <summary>
    ///     Provides API for customer data farmer.
    /// </summary>
    public interface INhomKhachHangDataFarmer : IDataFarmerBase<AccModels.NhomKhachHang>
    {
        /// <summary>
        ///     Gets list of customers satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<AccModels.NhomKhachHang> GetNhomKhachHangs(SearchCriteria criteria);
    }
}
