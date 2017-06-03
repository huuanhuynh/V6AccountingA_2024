using System;
using System.Linq;
using V6Soft.Accounting.Customer.Extensions;
using V6Soft.Accounting.ShippingMethod.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.ShippingMethod;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.ShippingMethod.Dealers
{
    /// <summary>
    ///     Provides ShippingMethodItem-related operations (shippingMethod CRUD, shippingMethod group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectShippingMethodDataDealer : IShippingMethodDataDealer
    {
        private ILogger m_Logger;
        private IShippingMethodDataFarmer m_ShippingMethodFarmer;

        public DirectShippingMethodDataDealer(ILogger logger, IShippingMethodDataFarmer shippingMethodFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(shippingMethodFarmer, "shippingMethodFarmer");

            m_Logger = logger;
            m_ShippingMethodFarmer = shippingMethodFarmer;
        }
        /// <summary>
        ///     See <see cref="IShippingMethodDataDealer.GetShippingMethods()"/>
        /// </summary>
        public PagedSearchResult<ShippingMethodListItem> GetShippingMethods(SearchCriteria criteria)
        {
            PagedSearchResult<ShippingMethodListItem> allShippingMethods = m_ShippingMethodFarmer.GetShippingMethods(criteria).ToShippingMethodViewModel();

            allShippingMethods.Data = allShippingMethods.Data
                .Select(item =>
                {
                    item.TenHinhThucVanChuyen = VnCodec.TCVNtoUNICODE(item.TenHinhThucVanChuyen);
                    item.TenHinhThucVanChuyen2 = VnCodec.TCVNtoUNICODE(item.TenHinhThucVanChuyen2);
                    return item;
                })
                .ToList();
            return allShippingMethods;
        }
        /// <summary>
        ///     See <see cref="IShippingMethodDataDealer.AddShippingMethod()"/>
        /// </summary>
        public bool AddShippingMethod(AccModels.ShippingMethod shippingMethod)
        {
            shippingMethod.CreatedDate = DateTime.Now;
            shippingMethod.ModifiedDate = DateTime.Now;
            shippingMethod.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            shippingMethod.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_ShippingMethodFarmer.Add(shippingMethod);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteShippingMethod(string key)
        {
            return m_ShippingMethodFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateShippingMethod(AccModels.ShippingMethod shippingMethod)
        {
            shippingMethod.CreatedDate = DateTime.Now;
            shippingMethod.ModifiedDate = DateTime.Now;
            shippingMethod.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            shippingMethod.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_ShippingMethodFarmer.Edit(shippingMethod);
        }
    }
}
