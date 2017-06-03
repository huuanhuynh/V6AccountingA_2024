using System;
using System.Linq;
using V6Soft.Accounting.Common.Farmers;
using V6Soft.Models.Accounting.DTO;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.System.Farmers
{
    /// <summary>
    ///     Provides API for customer data farmer.
    /// </summary>
    public interface IV6OptionDataFarmer : IDataFarmerBase<V6Option>
    {
        /// <summary>
        ///     Gets list of customers satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<V6Option> GetV6Options(SearchCriteria criteria);

        /// <summary>
        /// Get a customer by guid
        /// </summary>
        Models.Accounting.DTO.V6Option GetV6OptionById(Guid guid);

        // TODO: Should put this method in IDataFarmerBase
        /// <summary>
        /// 
        /// </summary>
        IQueryable<V6Option> AsQueryable();
    }
}
