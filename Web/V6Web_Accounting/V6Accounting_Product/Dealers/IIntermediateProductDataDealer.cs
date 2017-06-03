using V6Soft.Models.Accounting.ViewModels.IntermediateProduct;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.IntermediateProduct.Dealers
{
    /// <summary>
    ///     Acts as a service client to get intermediateProduct data from IntermediateProductService.
    /// </summary>
    public interface IIntermediateProductDataDealer
    {
        /// <summary>
        ///     Gets list of intermediateProducts satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<IntermediateProductListItem> GetIntermediateProducts(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new intermediateProduct.
        /// </summary>
        bool AddIntermediateProduct(AccModels.IntermediateProduct intermediateProduct);
        /// <summary>
        ///     Delete a intermediateProduct.
        /// </summary>
        bool DeleteIntermediateProduct(string key);
        /// <summary>
        ///     Update data for a intermediateProduct.
        /// </summary>
        bool UpdateIntermediateProduct(AccModels.IntermediateProduct intermediateProduct);
    }
}
