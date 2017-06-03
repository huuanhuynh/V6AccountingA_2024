using System;
using System.Linq;
using V6Soft.Accounting.Warehouse.Extensions;
using V6Soft.Accounting.Warehouse.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.Warehouse;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Warehouse.Dealers
{
    /// <summary>
    ///     Provides WarehouseItem-related operations (customer CRUD, customer group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectWarehouseDataDealer : IWarehouseDataDealer
    {
        private ILogger m_Logger;
        private IWarehouseDataFarmer m_WarehouseFarmer;

        public DirectWarehouseDataDealer(ILogger logger, IWarehouseDataFarmer customerFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_Logger = logger;
            m_WarehouseFarmer = customerFarmer;
        }
        /// <summary>
        ///     See <see cref="IWarehouseDataDealer.GetWarehouses()"/>
        /// </summary>
        public PagedSearchResult<WarehouseListItem> GetWarehouses(SearchCriteria criteria)
        {
            PagedSearchResult<WarehouseListItem> allWarehouses = m_WarehouseFarmer.GetWarehouses(criteria).ToWarehouseViewModel();

            allWarehouses.Data = allWarehouses.Data
                .Select(item =>
                {
                    item.TenKho = VnCodec.TCVNtoUNICODE(item.TenKho);
                    item.TenKho2 = VnCodec.TCVNtoUNICODE(item.TenKho2);
                    return item;
                })
                .ToList();
            return allWarehouses;
        }
        /// <summary>
        ///     See <see cref="IWarehouseDataDealer.AddWarehouse()"/>
        /// </summary>
        public bool AddWarehouse(AccModels.Warehouse customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_WarehouseFarmer.Add(customer);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteWarehouse(string key)
        {
            return m_WarehouseFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateWarehouse(AccModels.Warehouse customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_WarehouseFarmer.Edit(customer);
        }
    }
}
