using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Shipment.Dealers;
using V6Soft.Models.Accounting.ViewModels.Shipment;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/vanchuyens")]
    public class ShipmentController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IShipmentDataDealer m_Dealer;

        #endregion

        #region Construtor
        public ShipmentController(IShipmentDataDealer dealer)
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
        public PagedSearchResult<ShipmentListItem> GetShipments(SearchCriteria criteria)
        {
            PagedSearchResult<ShipmentListItem> result = m_Dealer.GetShipments(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddShipment(AccModels.Shipment vanchuyen)
        {
            vanchuyen.UID = NextUID;
            var result = m_Dealer.AddShipment(vanchuyen);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteShipment(string key)
        {
            m_Dealer.DeleteShipment(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateShipment(AccModels.Shipment vanchuyen)
        {
            m_Dealer.UpdateShipment(vanchuyen);
            return Ok();
        }
        #endregion
    }
}