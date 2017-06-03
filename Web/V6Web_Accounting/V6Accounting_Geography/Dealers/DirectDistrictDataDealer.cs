using System;
using System.Linq;
using V6Soft.Accounting.Geography.Dealers;
using V6Soft.Accounting.Geography.Extensions;
using V6Soft.Accounting.Geography.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.District;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.District.Dealers
{
    /// <summary>
    ///     Provides DistrictItem-related operations (district CRUD, district group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectDistrictDataDealer : IDistrictDataDealer
    {
        private ILogger m_Logger;
        private IDistrictDataFarmer m_DistrictFarmer;

        public DirectDistrictDataDealer(ILogger logger, IDistrictDataFarmer districtFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(districtFarmer, "districtFarmer");

            m_Logger = logger;
            m_DistrictFarmer = districtFarmer;
        }
        /// <summary>
        ///     See <see cref="IDistrictDataDealer.GetDistricts()"/>
        /// </summary>
        public PagedSearchResult<DistrictListItem> GetDistricts(SearchCriteria criteria)
        {
            PagedSearchResult<DistrictListItem> allDistricts = m_DistrictFarmer.GetDistricts(criteria).ToDistrictViewModel();

            allDistricts.Data = allDistricts.Data
                .Select(item =>
                {
                    item.Ten_quan = VnCodec.TCVNtoUNICODE(item.Ten_quan);
                    item.Ten_quan2 = VnCodec.TCVNtoUNICODE(item.Ten_quan2);
                    return item;
                })
                .ToList();
            return allDistricts;
        }
        /// <summary>
        ///     See <see cref="IDistrictDataDealer.AddDistrict()"/>
        /// </summary>
        public bool AddDistrict(AccModels.District district)
        {
            district.CreatedDate = DateTime.Now;
            district.ModifiedDate = DateTime.Now;
            district.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            district.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_DistrictFarmer.Add(district);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteDistrict(string key)
        {
            return m_DistrictFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateDistrict(AccModels.District district)
        {
            district.CreatedDate = DateTime.Now;
            district.ModifiedDate = DateTime.Now;
            district.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            district.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_DistrictFarmer.Edit(district);
        }
    }
}
