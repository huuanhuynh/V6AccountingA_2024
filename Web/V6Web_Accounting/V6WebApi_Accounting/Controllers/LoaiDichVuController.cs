using System.Net;
using System.Web.Http;
using V6Soft.Accounting.ServiceType.Dealers;
using V6Soft.Models.Accounting.ViewModels.ServiceType;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/loaidichvus")]
    public class ServiceTypeController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IServiceTypeDataDealer m_Dealer;

        #endregion

        #region Construtor
        public ServiceTypeController(IServiceTypeDataDealer dealer)
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
        public PagedSearchResult<ServiceTypeListItem> GetServiceTypes(SearchCriteria criteria)
        {
            PagedSearchResult<ServiceTypeListItem> result = m_Dealer.GetServiceTypes(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddServiceType(AccModels.ServiceType loaidichvu)
        {
            loaidichvu.UID = NextUID;
            var result = m_Dealer.AddServiceType(loaidichvu);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteServiceType(string key)
        {
            m_Dealer.DeleteServiceType(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateServiceType(AccModels.ServiceType loaidichvu)
        {
            m_Dealer.UpdateServiceType(loaidichvu);
            return Ok();
        }
        #endregion
    }
} 