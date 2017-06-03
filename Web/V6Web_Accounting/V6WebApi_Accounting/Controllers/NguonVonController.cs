using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Capital.Dealers;
using V6Soft.Models.Accounting.ViewModels.Capital;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/nguonvons")]
    public class CapitalController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly ICapitalDataDealer m_Dealer;

        #endregion

        #region Construtor
        public CapitalController(ICapitalDataDealer dealer)
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
        public PagedSearchResult<CapitalListItem> GetCapitals(SearchCriteria criteria)
        {
            PagedSearchResult<CapitalListItem> result = m_Dealer.GetCapitals(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddCapital(AccModels.Capital nguonvon)
        {
            nguonvon.UID = NextUID;
            var result = m_Dealer.AddCapital(nguonvon);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteCapital(string key)
        {
            m_Dealer.DeleteCapital(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateCapital(AccModels.Capital nguonvon)
        {
            m_Dealer.UpdateCapital(nguonvon);
            return Ok();
        }
        #endregion
    }
}