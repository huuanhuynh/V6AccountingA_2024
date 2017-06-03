using System;
using System.Linq;
using V6Soft.Accounting.MeasurementConversion.Farmers;
using V6Soft.Accounting.Product.Extensions;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.MeasurementConversion;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.MeasurementConversion.Dealers
{
    /// <summary>
    ///     Provides MeasurementConversionItem-related operations (customer CRUD, customer group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectMeasurementConversionDataDealer : IMeasurementConversionDataDealer
    {
        private ILogger m_Logger;
        private IMeasurementConversionDataFarmer m_MeasurementConversionFarmer;

        public DirectMeasurementConversionDataDealer(ILogger logger, IMeasurementConversionDataFarmer customerFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_Logger = logger;
            m_MeasurementConversionFarmer = customerFarmer;
        }
        /// <summary>
        ///     See <see cref="IMeasurementConversionDataDealer.GetMeasurementConversions()"/>
        /// </summary>
        public PagedSearchResult<MeasurementConversionListItem> GetMeasurementConversions(SearchCriteria criteria)
        {
            PagedSearchResult<MeasurementConversionListItem> allMeasurementConversions = m_MeasurementConversionFarmer.GetMeasurementConversions(criteria).ToMeasurementConversionViewModel();

            allMeasurementConversions.Data = allMeasurementConversions.Data
                .Select(item =>
                {
                    item.MaVatTu = VnCodec.TCVNtoUNICODE(item.MaVatTu);
                    return item;
                })
                .ToList();
            return allMeasurementConversions;
        }
        /// <summary>
        ///     See <see cref="IMeasurementConversionDataDealer.AddMeasurementConversion()"/>
        /// </summary>
        public bool AddMeasurementConversion(AccModels.MeasurementConversion customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_MeasurementConversionFarmer.Add(customer);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteMeasurementConversion(string key)
        {
            return m_MeasurementConversionFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateMeasurementConversion(AccModels.MeasurementConversion customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_MeasurementConversionFarmer.Edit(customer);
        }
    }
}
