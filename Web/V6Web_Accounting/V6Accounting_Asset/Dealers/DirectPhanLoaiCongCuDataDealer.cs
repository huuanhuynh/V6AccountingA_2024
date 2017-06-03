using System;
using System.Linq;
using V6Soft.Accounting.Asset.Extensions;
using V6Soft.Accounting.Asset.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.EquipmentType;
using V6Soft.Models.Accounting.ViewModels.PhanLoaiCongCu;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.EquipmentType.Dealers
{
    /// <summary>
    ///     Provides EquipmentTypeItem-related operations (equipmentType CRUD, equipmentType group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectEquipmentTypeDataDealer : IEquipmentTypeDataDealer
    {
        private ILogger m_Logger;
        private IPhanLoaiCongCuDataFarmer m_EquipmentTypeFarmer;

        public DirectEquipmentTypeDataDealer(ILogger logger, IPhanLoaiCongCuDataFarmer equipmentTypeFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(equipmentTypeFarmer, "equipmentTypeFarmer");

            m_Logger = logger;
            m_EquipmentTypeFarmer = equipmentTypeFarmer;
        }
        /// <summary>
        ///     See <see cref="IEquipmentTypeDataDealer.GetEquipmentTypes()"/>
        /// </summary>
        public PagedSearchResult<EquipmentTypeListItem> GetEquipmentTypes(SearchCriteria criteria)
        {
            PagedSearchResult<EquipmentTypeListItem> allEquipmentTypes = m_EquipmentTypeFarmer.GetPhanLoaiCongCus(criteria).ToPhanLoaiCongCuViewModel();

            allEquipmentTypes.Data = allEquipmentTypes.Data
                .Select(item =>
                {
                    item.MaLoai = VnCodec.TCVNtoUNICODE(item.MaLoai);
                    return item;
                })
                .ToList();
            return allEquipmentTypes;
        }
        /// <summary>
        ///     See <see cref="IEquipmentTypeDataDealer.AddEquipmentType()"/>
        /// </summary>
        public bool AddEquipmentType(AccModels.PhanLoaiCongCu equipmentType)
        {
            equipmentType.CreatedDate = DateTime.Now;
            equipmentType.ModifiedDate = DateTime.Now;
            equipmentType.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            equipmentType.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_EquipmentTypeFarmer.Add(equipmentType);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteEquipmentType(string key)
        {
            return m_EquipmentTypeFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateEquipmentType(AccModels.PhanLoaiCongCu equipmentType)
        {
            equipmentType.CreatedDate = DateTime.Now;
            equipmentType.ModifiedDate = DateTime.Now;
            equipmentType.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            equipmentType.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_EquipmentTypeFarmer.Edit(equipmentType);
        }
    }
}
