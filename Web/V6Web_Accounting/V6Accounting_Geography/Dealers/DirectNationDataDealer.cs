using System;
using System.Linq;
using V6Soft.Accounting.Geography.Extensions;
using V6Soft.Accounting.Geography.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.Nation;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Nation.Dealers
{
    /// <summary>
    ///     Provides NationItem-related operations (nation CRUD, nation group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectNationDataDealer : INationDataDealer
    {
        private ILogger m_Logger;
        private INationDataFarmer m_NationFarmer;

        public DirectNationDataDealer(ILogger logger, INationDataFarmer nationFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(nationFarmer, "nationFarmer");

            m_Logger = logger;
            m_NationFarmer = nationFarmer;
        }
        /// <summary>
        ///     See <see cref="INationDataDealer.GetNations()"/>
        /// </summary>
        public PagedSearchResult<NationListItem> GetNations(SearchCriteria criteria)
        {
            PagedSearchResult<NationListItem> allNations = m_NationFarmer.GetNations(criteria).ToNationViewModel();

            allNations.Data = allNations.Data
                .Select(item =>
                {
                    item.TenQuocGia = VnCodec.TCVNtoUNICODE(item.TenQuocGia);
                    item.TenQuocGia2 = VnCodec.TCVNtoUNICODE(item.TenQuocGia2);
                    return item;
                })
                .ToList();
            return allNations;
        }
        /// <summary>
        ///     See <see cref="INationDataDealer.AddNation()"/>
        /// </summary>
        public bool AddNation(AccModels.Nation nation)
        {
            nation.CreatedDate = DateTime.Now;
            nation.ModifiedDate = DateTime.Now;
            nation.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            nation.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_NationFarmer.Add(nation);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteNation(string key)
        {
            return m_NationFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateNation(AccModels.Nation nation)
        {
            nation.CreatedDate = DateTime.Now;
            nation.ModifiedDate = DateTime.Now;
            nation.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            nation.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_NationFarmer.Edit(nation);
        }
    }
}
