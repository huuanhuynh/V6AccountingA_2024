using System;
using System.Linq;
using V6Soft.Accounting.IntermediateProduct.Farmers;
using V6Soft.Accounting.Product.Extensions;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.IntermediateProduct;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.IntermediateProduct.Dealers
{
    /// <summary>
    ///     Provides IntermediateProductItem-related operations (intermediateProduct CRUD, intermediateProduct group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectIntermediateProductDataDealer : IIntermediateProductDataDealer
    {
        private ILogger m_Logger;
        private IIntermediateProductDataFarmer m_IntermediateProductFarmer;

        public DirectIntermediateProductDataDealer(ILogger logger, IIntermediateProductDataFarmer intermediateProductFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(intermediateProductFarmer, "intermediateProductFarmer");

            m_Logger = logger;
            m_IntermediateProductFarmer = intermediateProductFarmer;
        }
        /// <summary>
        ///     See <see cref="IIntermediateProductDataDealer.GetIntermediateProducts()"/>
        /// </summary>
        public PagedSearchResult<IntermediateProductListItem> GetIntermediateProducts(SearchCriteria criteria)
        {
            PagedSearchResult<IntermediateProductListItem> allIntermediateProducts = m_IntermediateProductFarmer.GetIntermediateProducts(criteria).ToIntermediateProductViewModel();

            allIntermediateProducts.Data = allIntermediateProducts.Data
                .Select(item =>
                {
                    item.TenVatTutg = VnCodec.TCVNtoUNICODE(item.TenVatTutg);
                    item.TenVatTutg2 = VnCodec.TCVNtoUNICODE(item.TenVatTutg2);
                    return item;
                })
                .ToList();
            return allIntermediateProducts;
        }
        /// <summary>
        ///     See <see cref="IIntermediateProductDataDealer.AddIntermediateProduct()"/>
        /// </summary>
        public bool AddIntermediateProduct(AccModels.IntermediateProduct intermediateProduct)
        {
            intermediateProduct.CreatedDate = DateTime.Now;
            intermediateProduct.ModifiedDate = DateTime.Now;
            intermediateProduct.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            intermediateProduct.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_IntermediateProductFarmer.Add(intermediateProduct);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteIntermediateProduct(string key)
        {
            return m_IntermediateProductFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateIntermediateProduct(AccModels.IntermediateProduct intermediateProduct)
        {
            intermediateProduct.CreatedDate = DateTime.Now;
            intermediateProduct.ModifiedDate = DateTime.Now;
            intermediateProduct.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            intermediateProduct.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_IntermediateProductFarmer.Edit(intermediateProduct);
        }
    }
}
