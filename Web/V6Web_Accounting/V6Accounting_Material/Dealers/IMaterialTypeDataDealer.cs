using V6Soft.Models.Accounting.ViewModels.MaterialType;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.MaterialType.Dealers
{
    /// <summary>
    ///     Acts as a service client to get materialType data from MaterialTypeService.
    /// </summary>
    public interface IMaterialTypeDataDealer
    {
        /// <summary>
        ///     Gets list of materialTypes satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<MaterialTypeItem> GetMaterialTypes(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new materialType.
        /// </summary>
        bool AddMaterialType(AccModels.MaterialType materialType);
        /// <summary>
        ///     Delete a materialType.
        /// </summary>
        bool DeleteMaterialType(string key);
        /// <summary>
        ///     Update data for a materialType.
        /// </summary>
        bool UpdateMaterialType(AccModels.MaterialType materialType);
    }
}
