using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Merchandise.Dealers;
using V6Soft.Models.Accounting.ViewModels.DanhMucLoHang;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/lohangs")]
    public class MerchandiseController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IMerchandiseDataDealer m_Dealer;

        #endregion

        #region Construtor
        public MerchandiseController(IMerchandiseDataDealer dealer)
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
        public PagedSearchResult<MerchandiseListItem> GetMerchandises(SearchCriteria criteria)
        {
            PagedSearchResult<MerchandiseListItem> result = m_Dealer.GetMerchandises(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddMerchandise(AccModels.Merchandise lohang)
        {
            lohang.UID = NextUID;
            var result = m_Dealer.AddMerchandise(lohang);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteMerchandise(string key)
        {
            m_Dealer.DeleteMerchandise(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateMerchandise(AccModels.Merchandise lohang)
        {
            m_Dealer.UpdateMerchandise(lohang);
            return Ok();
        }
        #endregion
    }
}