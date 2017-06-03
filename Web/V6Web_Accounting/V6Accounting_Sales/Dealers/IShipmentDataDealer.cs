using V6Soft.Models.Accounting.ViewModels.Shipment;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Shipment.Dealers
{
    /// <summary>
    ///     Acts as a service client to get shipment data from ShipmentService.
    /// </summary>
    public interface IShipmentDataDealer
    {
        /// <summary>
        ///     Gets list of shipments satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<ShipmentListItem> GetShipments(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new shipment.
        /// </summary>
        bool AddShipment(AccModels.Shipment shipment);
        /// <summary>
        ///     Delete a shipment.
        /// </summary>
        bool DeleteShipment(string key);
        /// <summary>
        ///     Update data for a shipment.
        /// </summary>
        bool UpdateShipment(AccModels.Shipment shipment);
    }
}
