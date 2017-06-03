using System;
using System.Linq;

using V6Soft.Accounting.Common.Farmers;
using V6Soft.Models.Accounting.ViewModels.Material;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Material.Farmers
{
    /// <summary>
    ///     Provides API for customer data farmer.
    /// </summary>
    public interface IMaterialDataFarmer : IDataFarmerBase<DTO.Material>
    {
        /// <summary>
        ///     Gets list of customers satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<DTO.Material> GetMaterials(SearchCriteria criteria);

        /// <summary>
        /// Get a customer by guid
        /// </summary>
        DTO.Material GetMaterialById(Guid guid);

        // TODO: Should put this method in IDataFarmerBase
        /// <summary>
        /// 
        /// </summary>
        IQueryable<DTO.Material> AsQueryable();
    }
}
