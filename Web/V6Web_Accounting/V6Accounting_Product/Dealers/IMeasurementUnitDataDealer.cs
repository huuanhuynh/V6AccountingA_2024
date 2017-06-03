using V6Soft.Models.Accounting.ViewModels.MeasurementUnit;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.MeasurementUnit.Dealers
{
    /// <summary>
    ///     Acts as a service client to get measurementUnit data from MeasurementUnitService.
    /// </summary>
    public interface IMeasurementUnitDataDealer
    {
        /// <summary>
        ///     Gets list of measurementUnits satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<MeasurementUnitListItem> GetMeasurementUnits(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new measurementUnit.
        /// </summary>
        bool AddMeasurementUnit(AccModels.MeasurementUnit measurementUnit);
        /// <summary>
        ///     Delete a measurementUnit.
        /// </summary>
        bool DeleteMeasurementUnit(string key);
        /// <summary>
        ///     Update data for a measurementUnit.
        /// </summary>
        bool UpdateMeasurementUnit(AccModels.MeasurementUnit measurementUnit);
    }
}
