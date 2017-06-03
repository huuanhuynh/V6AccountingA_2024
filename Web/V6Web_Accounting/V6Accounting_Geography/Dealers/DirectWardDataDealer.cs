using System;
using System.Linq;
using V6Soft.Accounting.Geography.Dealers;
using V6Soft.Accounting.Geography.Extensions;
using V6Soft.Accounting.Geography.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.Ward;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Ward.Dealers
{
    /// <summary>
    ///     Provides WardItem-related operations (ward CRUD, ward group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectWardDataDealer : IWardDataDealer
    {
        private ILogger m_Logger;
        private IWardDataFarmer m_WardFarmer;

        public DirectWardDataDealer(ILogger logger, IWardDataFarmer wardFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(wardFarmer, "wardFarmer");

            m_Logger = logger;
            m_WardFarmer = wardFarmer;
        }
        /// <summary>
        ///     See <see cref="IWardDataDealer.GetWards()"/>
        /// </summary>
        public PagedSearchResult<WardListItem> GetWards(SearchCriteria criteria)
        {
            PagedSearchResult<WardListItem> allWards = m_WardFarmer.GetWards(criteria).ToWardViewModel();

            allWards.Data = allWards.Data
                .Select(item =>
                {
                    item.TenPhuong = VnCodec.TCVNtoUNICODE(item.TenPhuong);
                    item.TenPhuong2 = VnCodec.TCVNtoUNICODE(item.TenPhuong2);
                    return item;
                })
                .ToList();
            return allWards;
        }
        /// <summary>
        ///     See <see cref="IWardDataDealer.AddWard()"/>
        /// </summary>
        public bool AddWard(AccModels.Ward ward)
        {
            ward.CreatedDate = DateTime.Now;
            ward.ModifiedDate = DateTime.Now;
            ward.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            ward.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_WardFarmer.Add(ward);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteWard(string key)
        {
            return m_WardFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateWard(AccModels.Ward ward)
        {
            ward.CreatedDate = DateTime.Now;
            ward.ModifiedDate = DateTime.Now;
            ward.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            ward.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_WardFarmer.Edit(ward);
        }
    }
}
