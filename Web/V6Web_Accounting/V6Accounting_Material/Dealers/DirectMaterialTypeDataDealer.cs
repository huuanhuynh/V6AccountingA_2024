using System;
using System.Linq;
using V6Soft.Accounting.Material.Extensions;
using V6Soft.Accounting.MaterialType.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.MaterialType;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.MaterialType.Dealers
{
    /// <summary>
    ///     Provides MaterialTypeItem-related operations (materialType CRUD, materialType group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectMaterialTypeDataDealer : IMaterialTypeDataDealer
    {
        private ILogger m_Logger;
        private IMaterialTypeDataFarmer m_MaterialTypeFarmer;

        public DirectMaterialTypeDataDealer(ILogger logger, IMaterialTypeDataFarmer materialTypeFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(materialTypeFarmer, "materialTypeFarmer");

            m_Logger = logger;
            m_MaterialTypeFarmer = materialTypeFarmer;
        }
        /// <summary>
        ///     See <see cref="IMaterialTypeDataDealer.GetMaterialTypes()"/>
        /// </summary>
        public PagedSearchResult<MaterialTypeItem> GetMaterialTypes(SearchCriteria criteria)
        {
            PagedSearchResult<MaterialTypeItem> allMaterialTypes = m_MaterialTypeFarmer.GetMaterialTypes(criteria).ToMaterialTypeViewModel();

            allMaterialTypes.Data = allMaterialTypes.Data
                .Select(item =>
                {
                    item.Ten_Loai = VnCodec.TCVNtoUNICODE(item.Ten_Loai);
                    item.Ten_Loai2 = VnCodec.TCVNtoUNICODE(item.Ten_Loai2);
                    return item;
                })
                .ToList();
            return allMaterialTypes;
        }
        /// <summary>
        ///     See <see cref="IMaterialTypeDataDealer.AddMaterialType()"/>
        /// </summary>
        public bool AddMaterialType(AccModels.MaterialType materialType)
        {
            materialType.CreatedDate = DateTime.Now;
            materialType.ModifiedDate = DateTime.Now;
            materialType.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            materialType.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_MaterialTypeFarmer.Add(materialType);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteMaterialType(string key)
        {
            return m_MaterialTypeFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateMaterialType(AccModels.MaterialType materialType)
        {
            materialType.CreatedDate = DateTime.Now;
            materialType.ModifiedDate = DateTime.Now;
            materialType.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            materialType.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_MaterialTypeFarmer.Edit(materialType);
        }
    }
}
