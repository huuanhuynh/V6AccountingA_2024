using System;
using V6Soft.Accounting.Common.Farmers;
using V6Soft.Models.Accounting.ViewModels.Shipment;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Shipment.Farmers
{
    /// <summary>
    ///     Provides API for customer data farmer.
    /// </summary>
    public interface IShipmentDataFarmer : IDataFarmerBase<AccModels.Shipment>
    {
        /// <summary>
        ///     Gets list of customers satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<AccModels.Shipment> GetShipments(SearchCriteria criteria);

        /// <summary>
        /// Get a customer by guid
        /// </summary>
        AccModels.Shipment GetShipmentById(Guid guid);
    }
}
