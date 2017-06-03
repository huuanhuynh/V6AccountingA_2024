using System.Net;
using System.Web.Http;
using V6Soft.Accounting.ShippingMethod.Dealers;
using V6Soft.Models.Accounting.ViewModels.ShippingMethod;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/hinhthucvanchuyens")]
    public class ShippingMethodController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IShippingMethodDataDealer m_Dealer;

        #endregion

        #region Construtor
        public ShippingMethodController(IShippingMethodDataDealer dealer)
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
        public PagedSearchResult<ShippingMethodListItem> GetShippingMethods(SearchCriteria criteria)
        {
            PagedSearchResult<ShippingMethodListItem> result = m_Dealer.GetShippingMethods(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddShippingMethod(AccModels.ShippingMethod hinhthucvanchuyen)
        {
            hinhthucvanchuyen.UID = NextUID;
            var result = m_Dealer.AddShippingMethod(hinhthucvanchuyen);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteShippingMethod(string key)
        {
            m_Dealer.DeleteShippingMethod(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateShippingMethod(AccModels.ShippingMethod hinhthucvanchuyen)
        {
            m_Dealer.UpdateShippingMethod(hinhthucvanchuyen);
            return Ok();
        }
        #endregion
    }
}