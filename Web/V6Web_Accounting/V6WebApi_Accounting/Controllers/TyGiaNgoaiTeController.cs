using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Currency.Dealers;
using V6Soft.Models.Accounting.ViewModels.ForeignExchangeRate;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/tygiangoaites")]
    public class ForeignExchangeRateController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IForeignExchangeRateDataDealer m_Dealer;

        #endregion

        #region Construtor
        public ForeignExchangeRateController(IForeignExchangeRateDataDealer dealer)
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
        public PagedSearchResult<ForeignExchangeRateListItem> GetForeignExchangeRates(SearchCriteria criteria)
        {
            PagedSearchResult<ForeignExchangeRateListItem> result = m_Dealer.GetForeignExchangeRates(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddForeignExchangeRate(AccModels.ForeignExchangeRate tygiangoaite)
        {
            tygiangoaite.UID = NextUID;
            var result = m_Dealer.AddForeignExchangeRate(tygiangoaite);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteForeignExchangeRate(string key)
        {
            m_Dealer.DeleteForeignExchangeRate(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateForeignExchangeRate(AccModels.ForeignExchangeRate tygiangoaite)
        {
            m_Dealer.UpdateForeignExchangeRate(tygiangoaite);
            return Ok();
        }
        #endregion
    }
} 