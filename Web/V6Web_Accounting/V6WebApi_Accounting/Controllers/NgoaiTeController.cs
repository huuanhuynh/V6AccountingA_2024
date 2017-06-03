using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Currency.Dealers;
using V6Soft.Accounting.ForeignCurrency.Dealers;
using V6Soft.Models.Accounting.ViewModels.ForeignCurrency;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/ngoaites")]
    public class ForeignCurrencyController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IForeignCurrencyDataDealer m_Dealer;

        #endregion

        #region Construtor
        public ForeignCurrencyController(IForeignCurrencyDataDealer dealer)
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
        public PagedSearchResult<ForeignCurrencyListItem> GetForeignCurrencys(SearchCriteria criteria)
        {
            PagedSearchResult<ForeignCurrencyListItem> result = m_Dealer.GetForeignCurrencys(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddForeignCurrency(AccModels.ForeignCurrency ngoaite)
        {
            ngoaite.UID = NextUID;
            var result = m_Dealer.AddForeignCurrency(ngoaite);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteForeignCurrency(string key)
        {
            m_Dealer.DeleteForeignCurrency(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateForeignCurrency(AccModels.ForeignCurrency ngoaite)
        {
            m_Dealer.UpdateForeignCurrency(ngoaite);
            return Ok();
        }
        #endregion
    }
} 