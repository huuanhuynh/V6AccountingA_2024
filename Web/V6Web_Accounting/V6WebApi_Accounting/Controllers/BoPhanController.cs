using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Department.Dealers;
using V6Soft.Models.Accounting.ViewModels.Department;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/bophans")]
    public class BoPhanController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IBoPhanDataDealer m_Dealer;

        #endregion

        #region Construtor
        public BoPhanController(IBoPhanDataDealer dealer)
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
        public PagedSearchResult<DepartmentListItem> GetBoPhans(SearchCriteria criteria)
        {
            PagedSearchResult<DepartmentListItem> result = m_Dealer.GetDepartments(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddBoPhan(Models.Accounting.DTO.Department bophan)
        {
            bophan.UID = NextUID;
            var result = m_Dealer.AddBoPhan(bophan);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteDepartment(string key)
        {
            m_Dealer.DeleteDepartment(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateDepartment(AccModels.Department bophan)
        {
            m_Dealer.UpdateDepartment(bophan);
            return Ok();
        }
        #endregion
    }
}