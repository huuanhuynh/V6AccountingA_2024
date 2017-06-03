using System.Net;
using System.Web.Http;
using V6Soft.Accounting.DiscountType.Dealers;
using V6Soft.Models.Accounting.ViewModels.DiscountType;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/loaichietkhaus")]
    public class DiscountTypeController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IDiscountTypeDataDealer m_Dealer;

        #endregion

        #region Construtor
        public DiscountTypeController(IDiscountTypeDataDealer dealer)
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
        public PagedSearchResult<DiscountTypeListItem> GetDiscountTypes(SearchCriteria criteria)
        {
            PagedSearchResult<DiscountTypeListItem> result = m_Dealer.GetDiscountTypes(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddDiscountType(AccModels.DiscountType loaichietkhau)
        {
            loaichietkhau.UID = NextUID;
            var result = m_Dealer.AddDiscountType(loaichietkhau);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteDiscountType(string key)
        {
            m_Dealer.DeleteDiscountType(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateDiscountType(AccModels.DiscountType loaichietkhau)
        {
            m_Dealer.UpdateDiscountType(loaichietkhau);
            return Ok();
        }
        #endregion
    }
} 