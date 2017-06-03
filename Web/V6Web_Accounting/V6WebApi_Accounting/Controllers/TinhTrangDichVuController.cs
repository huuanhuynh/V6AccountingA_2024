using System.Net;
using System.Web.Http;
using V6Soft.Accounting.ServiceStatus.Dealers;
using V6Soft.Models.Accounting.ViewModels.ServiceStatus;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/tinhtrangdichvus")]
    public class ServiceStatusController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IServiceStatusDataDealer m_Dealer;

        #endregion

        #region Construtor
        public ServiceStatusController(IServiceStatusDataDealer dealer)
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
        public PagedSearchResult<ServiceStatusListItem> GetServiceStatuss(SearchCriteria criteria)
        {
            PagedSearchResult<ServiceStatusListItem> result = m_Dealer.GetServiceStatuss(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddServiceStatus(AccModels.ServiceStatus tinhtrangdichvu)
        {
            tinhtrangdichvu.UID = NextUID;
            var result = m_Dealer.AddServiceStatus(tinhtrangdichvu);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteServiceStatus(string key)
        {
            m_Dealer.DeleteServiceStatus(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateServiceStatus(AccModels.ServiceStatus tinhtrangdichvu)
        {
            m_Dealer.UpdateServiceStatus(tinhtrangdichvu);
            return Ok();
        }
        #endregion
    }
}