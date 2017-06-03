using System.Net;
using System.Web.Http;
using V6Soft.Accounting.PriceCode.Dealers;
using V6Soft.Models.Accounting.ViewModels.PriceCode;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/danhmucmagias")]
    public class PriceCodeController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IPriceCodeDataDealer m_Dealer;

        #endregion

        #region Construtor
        public PriceCodeController(IPriceCodeDataDealer dealer)
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
        public PagedSearchResult<PriceCodeListItem> GetPriceCodes(SearchCriteria criteria)
        {
            PagedSearchResult<PriceCodeListItem> result = m_Dealer.GetPriceCodes(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddPriceCode(AccModels.PriceCode danhmucmagia)
        {
            danhmucmagia.UID = NextUID;
            var result = m_Dealer.AddPriceCode(danhmucmagia);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeletePriceCode(string key)
        {
            m_Dealer.DeletePriceCode(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdatePriceCode(AccModels.PriceCode danhmucmagia)
        {
            m_Dealer.UpdatePriceCode(danhmucmagia);
            return Ok();
        }
        #endregion
    }
} 