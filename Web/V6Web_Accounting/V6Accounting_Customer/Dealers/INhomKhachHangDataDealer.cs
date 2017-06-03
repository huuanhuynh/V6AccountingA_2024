using V6Soft.Models.Accounting.ViewModels.CustomerGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.NhomKhachHang.Dealers
{
    /// <summary>
    ///     Acts as a service client to get customer data from NhomKhachHangService.
    /// </summary>
    public interface INhomKhachHangDataDealer
    {
        /// <summary>
        ///     Gets list of customers satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<CustomerGroupListItem> GetNhomKhachHangs(SearchCriteria criteria);

        /// <summary>
        ///     Persists data for a new customer.
        /// </summary>
        bool AddNhomKhachHang(CustomerGroupDetail obj);
    }
}
