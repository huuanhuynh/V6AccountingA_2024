using V6Soft.Models.Accounting.ViewModels.EquipmentType;
using V6Soft.Models.Accounting.ViewModels.PhanLoaiCongCu;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.EquipmentType.Dealers
{
    /// <summary>
    ///     Acts as a service client to get equipmentType data from EquipmentTypeService.
    /// </summary>
    public interface IEquipmentTypeDataDealer
    {
        /// <summary>
        ///     Gets list of equipmentTypes satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<EquipmentTypeListItem> GetEquipmentTypes(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new equipmentType.
        /// </summary>
        bool AddEquipmentType(AccModels.PhanLoaiCongCu equipmentType);
        /// <summary>
        ///     Delete a equipmentType.
        /// </summary>
        bool DeleteEquipmentType(string key);
        /// <summary>
        ///     Update data for a equipmentType.
        /// </summary>
        bool UpdateEquipmentType(AccModels.PhanLoaiCongCu equipmentType);
    }
}
