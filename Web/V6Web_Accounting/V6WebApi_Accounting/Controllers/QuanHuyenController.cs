using System.Net;
using System.Web.Http;
using V6Soft.Accounting.District.Dealers;
using V6Soft.Models.Accounting.ViewModels.District;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/quanhuyens")]
    public class DistrictController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IDistrictDataDealer m_Dealer;

        #endregion

        #region Construtor
        public DistrictController(IDistrictDataDealer dealer)
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
        public PagedSearchResult<DistrictListItem> GetDistricts(SearchCriteria criteria)
        {
            PagedSearchResult<DistrictListItem> result = m_Dealer.GetDistricts(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddDistrict(AccModels.District quanhuyen)
        {
            quanhuyen.UID = NextUID;
            var result = m_Dealer.AddDistrict(quanhuyen);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteDistrict(string key)
        {
            m_Dealer.DeleteDistrict(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateDistrict(AccModels.District quanhuyen)
        {
            m_Dealer.UpdateDistrict(quanhuyen);
            return Ok();
        }
        #endregion
    }
} 