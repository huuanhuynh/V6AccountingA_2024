using System.Net;
using System.Web.Http;
using V6Soft.Accounting.IntermediateProduct.Dealers;
using V6Soft.Models.Accounting.ViewModels.IntermediateProduct;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/sanphamtrunggians")]
    public class IntermediateProductController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IIntermediateProductDataDealer m_Dealer;

        #endregion

        #region Construtor
        public IntermediateProductController(IIntermediateProductDataDealer dealer)
        {
            m_Dealer = dealer;
        }
        #endregion

        #region Ping
        [HttpGet]
        public IHttpActionResult Ping()
        {
            object result;
            if (m_Dealer != null)
            {
                result = new { Resolved = true };
            }
            else
            {
                result = new { Resolved = false };
            }
            return Ok(result);
        }
        #endregion

        #region Getting
        [HttpPost]
        [Route("list")]
        public PagedSearchResult<IntermediateProductListItem> GetIntermediateProducts(SearchCriteria criteria)
        {
            PagedSearchResult<IntermediateProductListItem> result = m_Dealer.GetIntermediateProducts(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddIntermediateProduct(AccModels.IntermediateProduct sanphamtrunggian)
        {
            sanphamtrunggian.UID = NextUID;
            var result = m_Dealer.AddIntermediateProduct(sanphamtrunggian);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteIntermediateProduct(string key)
        {
            m_Dealer.DeleteIntermediateProduct(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateIntermediateProduct(AccModels.IntermediateProduct sanphamtrunggian)
        {
            m_Dealer.UpdateIntermediateProduct(sanphamtrunggian);
            return Ok();
        }
        #endregion
    }
}