using System;
using V6Soft.Accounting.Common.Farmers;
using V6Soft.Models.Accounting.ViewModels.LoaiNhapXuat;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.LoaiNhapXuat.Farmers
{
    /// <summary>
    ///     Provides API for customer data farmer.
    /// </summary>
    public interface ILoaiNhapXuatDataFarmer : IDataFarmerBase<AccModels.LoaiNhapXuat>
    {
        /// <summary>
        ///     Gets list of customers satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<AccModels.LoaiNhapXuat> GetLoaiNhapXuats(SearchCriteria criteria);

        /// <summary>
        /// Get a customer by guid
        /// </summary>
        AccModels.LoaiNhapXuat GetLoaiNhapXuatById(Guid guid);
    }
}
