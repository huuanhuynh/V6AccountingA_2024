using System;
using System.Linq;
using V6Soft.Accounting.Material.Extensions;
using V6Soft.Accounting.Material.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.MaterialGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.MaterialGroup.Dealers
{
    /// <summary>
    ///     Provides MaterialGroupItem-related operations (materialGroup CRUD, materialGroup group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectMaterialGroupDataDealer : IMaterialGroupDataDealer
    {
        private ILogger m_Logger;
        private IMaterialGroupDataFarmer m_MaterialGroupFarmer;

        public DirectMaterialGroupDataDealer(ILogger logger, IMaterialGroupDataFarmer materialGroupFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(materialGroupFarmer, "materialGroupFarmer");

            m_Logger = logger;
            m_MaterialGroupFarmer = materialGroupFarmer;
        }
        /// <summary>
        ///     See <see cref="IMaterialGroupDataDealer.GetMaterialGroups()"/>
        /// </summary>
        public PagedSearchResult<MaterialGroupListItem> GetMaterialGroups(SearchCriteria criteria)
        {
            PagedSearchResult<MaterialGroupListItem> allMaterialGroups = m_MaterialGroupFarmer.GetMaterialGroups(criteria).ToMaterialGroupViewModel();

            allMaterialGroups.Data = allMaterialGroups.Data
                .Select(item =>
                {
                    item.TenNhom = VnCodec.TCVNtoUNICODE(item.TenNhom);
                    item.TenNhom2 = VnCodec.TCVNtoUNICODE(item.TenNhom2);
                    return item;
                })
                .ToList();
            return allMaterialGroups;
        }
        /// <summary>
        ///     See <see cref="IMaterialGroupDataDealer.AddMaterialGroup()"/>
        /// </summary>
        public bool AddMaterialGroup(AccModels.MaterialGroup materialGroup)
        {
            materialGroup.CreatedDate = DateTime.Now;
            materialGroup.ModifiedDate = DateTime.Now;
            materialGroup.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            materialGroup.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_MaterialGroupFarmer.Add(materialGroup);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteMaterialGroup(string key)
        {
            return m_MaterialGroupFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateMaterialGroup(AccModels.MaterialGroup materialGroup)
        {
            materialGroup.CreatedDate = DateTime.Now;
            materialGroup.ModifiedDate = DateTime.Now;
            materialGroup.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            materialGroup.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_MaterialGroupFarmer.Edit(materialGroup);
        }
    }
}
