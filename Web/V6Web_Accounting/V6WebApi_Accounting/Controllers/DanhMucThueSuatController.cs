using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Tax.Dealers;
using V6Soft.Models.Accounting.ViewModels.Tax;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/danhmucthuesuats")]
    public class TaxController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly ITaxDataDealer m_Dealer;

        #endregion

        #region Construtor
        public TaxController(ITaxDataDealer dealer)
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
        public PagedSearchResult<TaxListItem> GetTaxs(SearchCriteria criteria)
        {
            PagedSearchResult<TaxListItem> result = m_Dealer.GetTaxs(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddTax(AccModels.Tax danhmucthuesuat)
        {
            danhmucthuesuat.UID = NextUID;
            var result = m_Dealer.AddTax(danhmucthuesuat);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteTax(string key)
        {
            m_Dealer.DeleteTax(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateTax(AccModels.Tax danhmucthuesuat)
        {
            m_Dealer.UpdateTax(danhmucthuesuat);
            return Ok();
        }
        #endregion
    }
}