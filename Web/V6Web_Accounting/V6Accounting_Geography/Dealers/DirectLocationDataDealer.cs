using System;
using System.Linq;
using V6Soft.Accounting.Geography.Extensions;
using V6Soft.Accounting.Geography.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.Location;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Location.Dealers
{
    /// <summary>
    ///     Provides LocationItem-related operations (location CRUD, location group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectLocationDataDealer : ILocationDataDealer
    {
        private ILogger m_Logger;
        private ILocationDataFarmer m_LocationFarmer;

        public DirectLocationDataDealer(ILogger logger, ILocationDataFarmer locationFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(locationFarmer, "locationFarmer");

            m_Logger = logger;
            m_LocationFarmer = locationFarmer;
        }
        /// <summary>
        ///     See <see cref="ILocationDataDealer.GetLocations()"/>
        /// </summary>
        public PagedSearchResult<LocationListItem> GetLocations(SearchCriteria criteria)
        {
            PagedSearchResult<LocationListItem> allLocations = m_LocationFarmer.GetLocations(criteria).ToLocationViewModel();

            allLocations.Data = allLocations.Data
                .Select(item =>
                {
                    item.TenViTri = VnCodec.TCVNtoUNICODE(item.TenViTri);
                    item.TenViTri2 = VnCodec.TCVNtoUNICODE(item.TenViTri2);
                    return item;
                })
                .ToList();
            return allLocations;
        }
        /// <summary>
        ///     See <see cref="ILocationDataDealer.AddLocation()"/>
        /// </summary>
        public bool AddLocation(AccModels.Location location)
        {
            location.CreatedDate = DateTime.Now;
            location.ModifiedDate = DateTime.Now;
            location.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            location.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_LocationFarmer.Add(location);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteLocation(string key)
        {
            return m_LocationFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateLocation(AccModels.Location location)
        {
            location.CreatedDate = DateTime.Now;
            location.ModifiedDate = DateTime.Now;
            location.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            location.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_LocationFarmer.Edit(location);
        }
    }
}
