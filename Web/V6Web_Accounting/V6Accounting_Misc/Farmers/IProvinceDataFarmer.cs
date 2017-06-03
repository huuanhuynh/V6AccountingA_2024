using System;
using V6Soft.Accounting.Common.Farmers;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Misc.Farmers
{
    /// <summary>
    ///     Provides API for customer data farmer.
    /// </summary>
    public interface IProvinceDataFarmer : IDataFarmerBase<Models.Accounting.DTO.Province>
    {
        /// <summary>
        ///     Gets list of customers satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<Models.Accounting.DTO.Province> GetProvinces(SearchCriteria criteria);

        /// <summary>
        /// Get a customer by guid
        /// </summary>
        Models.Accounting.DTO.Province GetProvinceById(Guid guid);
    }
}
