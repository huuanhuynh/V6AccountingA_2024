using System;
using System.Linq;
using V6Soft.Accounting.Customer.Extensions;
using V6Soft.Accounting.Shipment.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.Shipment;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Shipment.Dealers
{
    /// <summary>
    ///     Provides ShipmentItem-related operations (shipment CRUD, shipment group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectShipmentDataDealer : IShipmentDataDealer
    {
        private ILogger m_Logger;
        private IShipmentDataFarmer m_ShipmentFarmer;

        public DirectShipmentDataDealer(ILogger logger, IShipmentDataFarmer shipmentFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(shipmentFarmer, "shipmentFarmer");

            m_Logger = logger;
            m_ShipmentFarmer = shipmentFarmer;
        }
        /// <summary>
        ///     See <see cref="IShipmentDataDealer.GetShipments()"/>
        /// </summary>
        public PagedSearchResult<ShipmentListItem> GetShipments(SearchCriteria criteria)
        {
            PagedSearchResult<ShipmentListItem> allShipments = m_ShipmentFarmer.GetShipments(criteria).ToShipmentViewModel();

            allShipments.Data = allShipments.Data
                .Select(item =>
                {
                    item.TenVanChuyen = VnCodec.TCVNtoUNICODE(item.TenVanChuyen);
                    item.TenVanChuyen2 = VnCodec.TCVNtoUNICODE(item.TenVanChuyen2);
                    return item;
                })
                .ToList();
            return allShipments;
        }
        /// <summary>
        ///     See <see cref="IShipmentDataDealer.AddShipment()"/>
        /// </summary>
        public bool AddShipment(AccModels.Shipment shipment)
        {
            shipment.CreatedDate = DateTime.Now;
            shipment.ModifiedDate = DateTime.Now;
            shipment.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            shipment.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_ShipmentFarmer.Add(shipment);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteShipment(string key)
        {
            return m_ShipmentFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateShipment(AccModels.Shipment shipment)
        {
            shipment.CreatedDate = DateTime.Now;
            shipment.ModifiedDate = DateTime.Now;
            shipment.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            shipment.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_ShipmentFarmer.Edit(shipment);
        }
    }
}
