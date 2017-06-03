using System;
using System.Linq;
using V6Soft.Accounting.MeasurementUnit.Farmers;
using V6Soft.Accounting.Product.Extensions;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.MeasurementUnit;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.MeasurementUnit.Dealers
{
    /// <summary>
    ///     Provides MeasurementUnitItem-related operations (measurementUnit CRUD, measurementUnit group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectMeasurementUnitDataDealer : IMeasurementUnitDataDealer
    {
        private ILogger m_Logger;
        private IMeasurementUnitDataFarmer m_MeasurementUnitFarmer;

        public DirectMeasurementUnitDataDealer(ILogger logger, IMeasurementUnitDataFarmer measurementUnitFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(measurementUnitFarmer, "measurementUnitFarmer");

            m_Logger = logger;
            m_MeasurementUnitFarmer = measurementUnitFarmer;
        }
        /// <summary>
        ///     See <see cref="IMeasurementUnitDataDealer.GetMeasurementUnits()"/>
        /// </summary>
        public PagedSearchResult<MeasurementUnitListItem> GetMeasurementUnits(SearchCriteria criteria)
        {
            PagedSearchResult<MeasurementUnitListItem> allMeasurementUnits = m_MeasurementUnitFarmer.GetMeasurementUnits(criteria).ToMeasurementUnitViewModel();

            allMeasurementUnits.Data = allMeasurementUnits.Data
                .Select(item =>
                {
                    item.Ten_DonViTinh = VnCodec.TCVNtoUNICODE(item.Ten_DonViTinh);
                    item.Ten_DonViTinh2 = VnCodec.TCVNtoUNICODE(item.Ten_DonViTinh2);
                    return item;
                })
                .ToList();
            return allMeasurementUnits;
        }
        /// <summary>
        ///     See <see cref="IMeasurementUnitDataDealer.AddMeasurementUnit()"/>
        /// </summary>
        public bool AddMeasurementUnit(AccModels.MeasurementUnit measurementUnit)
        {
            measurementUnit.CreatedDate = DateTime.Now;
            measurementUnit.ModifiedDate = DateTime.Now;
            measurementUnit.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            measurementUnit.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_MeasurementUnitFarmer.Add(measurementUnit);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteMeasurementUnit(string key)
        {
            return m_MeasurementUnitFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateMeasurementUnit(AccModels.MeasurementUnit measurementUnit)
        {
            measurementUnit.CreatedDate = DateTime.Now;
            measurementUnit.ModifiedDate = DateTime.Now;
            measurementUnit.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            measurementUnit.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_MeasurementUnitFarmer.Edit(measurementUnit);
        }
    }
}
