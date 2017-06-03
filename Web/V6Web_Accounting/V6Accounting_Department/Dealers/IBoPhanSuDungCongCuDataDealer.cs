using V6Soft.Models.Accounting.ViewModels.BoPhanSuDungCongCu;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Department.Dealers
{
    /// <summary>
    ///     Acts as a service client to get customer data from BoPhanSuDungCongCuService.
    /// </summary>
    public interface IBoPhanSuDungCongCuDataDealer
    {
        /// <summary>
        ///     Gets list of customers satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<BoPhanSuDungCongCuItem> GetBoPhanSuDungCongCus(SearchCriteria criteria);

        /// <summary>
        ///     Persists data for a new customer.
        /// </summary>
        bool AddBoPhanSuDungCongCu(Models.Accounting.DTO.BoPhanSuDungCongCu department);
        bool DeleteBoPhanSuDungCongCu(string key);
        /// <summary>
        ///     Update data for a customer.
        /// </summary>
        bool UpdateBoPhanSuDungCongCu(Models.Accounting.DTO.BoPhanSuDungCongCu department);

    }
}
