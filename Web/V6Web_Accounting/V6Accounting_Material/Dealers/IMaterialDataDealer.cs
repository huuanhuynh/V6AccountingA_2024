using V6Soft.Accounting.Common.Dealers;
using V6Soft.Models.Accounting.ViewModels.Material;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using DTO = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Material.Dealers
{
    /// <summary>
    ///     Acts as a service client to get material data from MaterialService.
    /// </summary>
    public interface IMaterialDataDealer : IODataFriendly<DTO.Material>
    {
        /// <summary>
        ///     Gets list of materials satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<MaterialListItem> GetMaterials(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new material.
        /// </summary>
        bool AddMaterial(DTO.Material material);
        /// <summary>
        ///     Delete a material.
        /// </summary>
        bool DeleteMaterial(string key);
        /// <summary>
        ///     Update data for a material.
        /// </summary>
        bool UpdateMaterial(DTO.Material material);
    }
}
