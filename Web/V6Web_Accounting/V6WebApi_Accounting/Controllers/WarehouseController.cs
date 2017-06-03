using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Warehouse.Dealers;
using V6Soft.Models.Accounting.ViewModels.Warehouse;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/khohangs")]
    public class WarehouseController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IWarehouseDataDealer m_Dealer;

        #endregion

        #region Construtor
        public WarehouseController(IWarehouseDataDealer dealer)
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
        public PagedSearchResult<WarehouseListItem> GetWarehouses(SearchCriteria criteria)
        {
            PagedSearchResult<WarehouseListItem> result = m_Dealer.GetWarehouses(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddWarehouse(AccModels.Warehouse khohang)
        {
            khohang.UID = NextUID;
            var result = m_Dealer.AddWarehouse(khohang);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteWarehouse(string key)
        {
            m_Dealer.DeleteWarehouse(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateWarehouse(AccModels.Warehouse khohang)
        {
            m_Dealer.UpdateWarehouse(khohang);
            return Ok();
        }
        #endregion
    }
}