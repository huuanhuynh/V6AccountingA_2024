using System;
using System.Linq;

using V6Soft.Accounting.Common.Farmers;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.BoPhan.Farmers
{
    /// <summary>
    ///     Provides API for BoPhan data farmer.
    /// </summary>
    public interface IBoPhanDataFarmer : IDataFarmerBase<DTO.Department>
    {
        /// <summary>
        ///     Gets list of BoPhans satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<DTO.Department> GetBoPhans(SearchCriteria criteria);

        /// <summary>
        /// Get a BoPhan by guid
        /// </summary>
        DTO.Department GetBoPhanById(Guid guid);

        // TODO: Should put this method in IDataFarmerBase
        /// <summary>
        /// 
        /// </summary>
        IQueryable<DTO.Department> AsQueryable();
    }
}
