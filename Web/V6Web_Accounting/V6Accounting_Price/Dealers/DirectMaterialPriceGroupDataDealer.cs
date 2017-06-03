using System;
using System.Linq;
using V6Soft.Accounting.Customer.Extensions;
using V6Soft.Accounting.MaterialPriceGroup.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.MaterialPriceGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.MaterialPriceGroup.Dealers
{
    /// <summary>
    ///     Provides MaterialPriceGroupItem-related operations (materialPriceGroup CRUD, materialPriceGroup group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectMaterialPriceGroupDataDealer : IMaterialPriceGroupDataDealer
    {
        private ILogger m_Logger;
        private IMaterialPriceGroupDataFarmer m_MaterialPriceGroupFarmer;

        public DirectMaterialPriceGroupDataDealer(ILogger logger, IMaterialPriceGroupDataFarmer materialPriceGroupFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(materialPriceGroupFarmer, "materialPriceGroupFarmer");

            m_Logger = logger;
            m_MaterialPriceGroupFarmer = materialPriceGroupFarmer;
        }
        /// <summary>
        ///     See <see cref="IMaterialPriceGroupDataDealer.GetMaterialPriceGroups()"/>
        /// </summary>
        public PagedSearchResult<MaterialPriceGroupListItem> GetMaterialPriceGroups(SearchCriteria criteria)
        {
            PagedSearchResult<MaterialPriceGroupListItem> allMaterialPriceGroups = m_MaterialPriceGroupFarmer.GetMaterialPriceGroups(criteria).ToMaterialPriceGroupViewModel();

            allMaterialPriceGroups.Data = allMaterialPriceGroups.Data
                .Select(item =>
                {
                    item.MaNhom = VnCodec.TCVNtoUNICODE(item.MaNhom);
                    item.TenNhom = VnCodec.TCVNtoUNICODE(item.TenNhom);
                    return item;
                })
                .ToList();
            return allMaterialPriceGroups;
        }
        /// <summary>
        ///     See <see cref="IMaterialPriceGroupDataDealer.AddMaterialPriceGroup()"/>
        /// </summary>
        public bool AddMaterialPriceGroup(AccModels.MaterialPriceGroup materialPriceGroup)
        {
            materialPriceGroup.CreatedDate = DateTime.Now;
            materialPriceGroup.ModifiedDate = DateTime.Now;
            materialPriceGroup.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            materialPriceGroup.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_MaterialPriceGroupFarmer.Add(materialPriceGroup);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteMaterialPriceGroup(string key)
        {
            return m_MaterialPriceGroupFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateMaterialPriceGroup(AccModels.MaterialPriceGroup materialPriceGroup)
        {
            materialPriceGroup.CreatedDate = DateTime.Now;
            materialPriceGroup.ModifiedDate = DateTime.Now;
            materialPriceGroup.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            materialPriceGroup.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_MaterialPriceGroupFarmer.Edit(materialPriceGroup);
        }
    }
}
