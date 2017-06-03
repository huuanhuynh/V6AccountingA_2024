using System;
using System.Linq;

using V6Soft.Accounting.Common.Farmers;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.PriceCode.Farmers
{
    /// <summary>
    ///     Provides API for PriceCode data farmer.
    /// </summary>
    public interface IPriceCodeDataFarmer : IDataFarmerBase<DTO.PriceCode>
    {
        /// <summary>
        ///     Gets list of PriceCodes satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<DTO.PriceCode> GetPriceCodes(SearchCriteria criteria);

        /// <summary>
        /// Get a PriceCode by guid
        /// </summary>
        DTO.PriceCode GetPriceCodeById(Guid guid);

        // TODO: Should put this method in IDataFarmerBase
        /// <summary>
        /// 
        /// </summary>
        IQueryable<DTO.PriceCode> AsQueryable();
    }
}
