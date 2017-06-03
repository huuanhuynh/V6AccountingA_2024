using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Discount.Dealers;
using V6Soft.Models.Accounting.ViewModels.Discount;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/chietkhaus")]
    public class DiscountController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IDiscountDataDealer m_Dealer;

        #endregion

        #region Construtor
        public DiscountController(IDiscountDataDealer dealer)
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
        public PagedSearchResult<DiscountListItem> GetDiscounts(SearchCriteria criteria)
        {
            PagedSearchResult<DiscountListItem> result = m_Dealer.GetDiscounts(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddDiscount(AccModels.Discount chietkhau)
        {
            chietkhau.UID = NextUID;
            var result = m_Dealer.AddDiscount(chietkhau);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteDiscount(string key)
        {
            m_Dealer.DeleteDiscount(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateDiscount(AccModels.Discount chietkhau)
        {
            m_Dealer.UpdateDiscount(chietkhau);
            return Ok();
        }
        #endregion
    }
}