using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Location.Dealers;
using V6Soft.Models.Accounting.ViewModels.Location;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/vitris")]
    public class LocationController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly ILocationDataDealer m_Dealer;

        #endregion

        #region Construtor
        public LocationController(ILocationDataDealer dealer)
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
        public PagedSearchResult<LocationListItem> GetLocations(SearchCriteria criteria)
        {
            PagedSearchResult<LocationListItem> result = m_Dealer.GetLocations(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddLocation(AccModels.Location vitri)
        {
            vitri.UID = NextUID;
            var result = m_Dealer.AddLocation(vitri);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteLocation(string key)
        {
            m_Dealer.DeleteLocation(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateLocation(AccModels.Location vitri)
        {
            m_Dealer.UpdateLocation(vitri);
            return Ok();
        }
        #endregion
    }
}