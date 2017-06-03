using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using V6Soft.Accounting.Common.Dealers;
using V6Soft.Accounting.Material.Extensions;
using V6Soft.Accounting.Material.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.Material;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Material.Dealers
{
    /// <summary>
    ///     Provides MaterialItem-related operations (material CRUD, material group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectMaterialDataDealer : DataDealerBase, IMaterialDataDealer
    {
        private ILogger m_Logger;
        private IMaterialDataFarmer m_MaterialFarmer;

        public DirectMaterialDataDealer(ILogger logger, IMaterialDataFarmer materialFarmer)
            : base(materialFarmer.AsQueryable())
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(materialFarmer, "materialFarmer");

            m_Logger = logger;
            m_MaterialFarmer = materialFarmer;
        }
        /// <summary>
        ///     See <see cref="IMaterialDataDealer.GetMaterials()"/>
        /// </summary>
        public PagedSearchResult<MaterialListItem> GetMaterials(SearchCriteria criteria)
        {
            PagedSearchResult<MaterialListItem> allMaterials = m_MaterialFarmer.GetMaterials(criteria).ToMaterialViewModel();

            allMaterials.Data = allMaterials.Data
                .Select(item =>
                {
                    item.TenVatTu = VnCodec.TCVNtoUNICODE(item.TenVatTu);
                    return item;
                })
                .ToList();
            return allMaterials;
        }
        /// <summary>
        ///     See <see cref="IMaterialDataDealer.AddMaterial()"/>
        /// </summary>
        public bool AddMaterial(DTO.Material material)
        {
            material.CreatedDate = DateTime.Now;
            material.ModifiedDate = DateTime.Now;
            material.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            material.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_MaterialFarmer.Add(material);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteMaterial(string key)
        {
            return m_MaterialFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateMaterial(DTO.Material material)
        {
            material.CreatedDate = DateTime.Now;
            material.ModifiedDate = DateTime.Now;
            material.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            material.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_MaterialFarmer.Edit(material);
        }


        /// <summary>
        ///     See <see cref="IODataFriendly.AsQueryable"/>
        /// </summary>
        public IQueryable<DTO.Material> AsQueryable()
        {
            return m_QueryProvider.CreateQuery<DTO.Material>();
        }

        public void Save(IList<DynamicObject> models)
        {
            
        }
    }
}
