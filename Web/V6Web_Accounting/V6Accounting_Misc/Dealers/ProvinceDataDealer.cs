using System;
using System.Linq;
using V6Soft.Accounting.Customer.Extensions;
using V6Soft.Accounting.Misc.Dealers;
using V6Soft.Accounting.Misc.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.Province;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Province.Dealers
{
    /// <summary>
    ///     Provides ProvinceItem-related operations (province CRUD, province group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectProvinceDataDealer : IProvinceDataDealer
    {
        private ILogger m_Logger;
        private IProvinceDataFarmer m_ProvinceFarmer;

        public DirectProvinceDataDealer(ILogger logger, IProvinceDataFarmer provinceFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(provinceFarmer, "provinceFarmer");

            m_Logger = logger;
            m_ProvinceFarmer = provinceFarmer;
        }
        /// <summary>
        ///     See <see cref="IProvinceDataDealer.GetProvinces()"/>
        /// </summary>
        public PagedSearchResult<ProvinceListItem> GetProvinces(SearchCriteria criteria)
        {
            PagedSearchResult<ProvinceListItem> allProvinces = m_ProvinceFarmer.GetProvinces(criteria).ToProvinceViewModel();

            allProvinces.Data = allProvinces.Data
                .Select(item =>
                {
                    return item;
                })
                .ToList();
            return allProvinces;
        }
        /// <summary>
        ///     See <see cref="IProvinceDataDealer.AddProvince()"/>
        /// </summary>
        public bool AddProvince(AccModels.Province province)
        {
            province.CreatedDate = DateTime.Now;
            province.ModifiedDate = DateTime.Now;
            province.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            province.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_ProvinceFarmer.Add(province);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteProvince(string key)
        {
            return m_ProvinceFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateProvince(AccModels.Province province)
        {
            province.CreatedDate = DateTime.Now;
            province.ModifiedDate = DateTime.Now;
            province.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            province.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_ProvinceFarmer.Edit(province);
        }
    }
}
