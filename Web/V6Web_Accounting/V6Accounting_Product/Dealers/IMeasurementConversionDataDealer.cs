using V6Soft.Models.Accounting.ViewModels.MeasurementConversion;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.MeasurementConversion.Dealers
{
    /// <summary>
    ///     Acts as a service client to get measurementConversion data from MeasurementConversionService.
    /// </summary>
    public interface IMeasurementConversionDataDealer
    {
        /// <summary>
        ///     Gets list of measurementConversions satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<MeasurementConversionListItem> GetMeasurementConversions(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new measurementConversion.
        /// </summary>
        bool AddMeasurementConversion(AccModels.MeasurementConversion measurementConversion);
        /// <summary>
        ///     Delete a measurementConversion.
        /// </summary>
        bool DeleteMeasurementConversion(string key);
        /// <summary>
        ///     Update data for a measurementConversion.
        /// </summary>
        bool UpdateMeasurementConversion(AccModels.MeasurementConversion measurementConversion);
    }
}
