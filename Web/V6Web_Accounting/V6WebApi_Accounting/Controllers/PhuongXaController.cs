using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Geography.Dealers;
using V6Soft.Models.Accounting.ViewModels.Ward;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/phuongxas")]
    public class WardController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IWardDataDealer m_Dealer;

        #endregion

        #region Construtor
        public WardController(IWardDataDealer dealer)
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
        public PagedSearchResult<WardListItem> GetWards(SearchCriteria criteria)
        {
            PagedSearchResult<WardListItem> result = m_Dealer.GetWards(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddWard(AccModels.Ward phuongxa)
        {
            phuongxa.UID = NextUID;
            var result = m_Dealer.AddWard(phuongxa);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteWard(string key)
        {
            m_Dealer.DeleteWard(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateWard(AccModels.Ward phuongxa)
        {
            m_Dealer.UpdateWard(phuongxa);
            return Ok();
        }
        #endregion
    }
} 