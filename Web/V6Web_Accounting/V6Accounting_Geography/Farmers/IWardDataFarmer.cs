using System;
using V6Soft.Accounting.Common.Farmers;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Geography.Farmers
{
    /// <summary>
    ///     Provides API for customer data farmer.
    /// </summary>
    public interface IWardDataFarmer : IDataFarmerBase<Models.Accounting.DTO.Ward>
    {
        /// <summary>
        ///     Gets list of customers satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<Models.Accounting.DTO.Ward> GetWards(SearchCriteria criteria);

        /// <summary>
        /// Get a customer by guid
        /// </summary>
        Models.Accounting.DTO.Ward GetWardById(Guid guid);
    }
}
