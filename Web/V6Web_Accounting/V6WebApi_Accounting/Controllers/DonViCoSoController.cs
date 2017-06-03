using System.Net;
using System.Web.Http;
using V6Soft.Accounting.BranchUnit.Dealers;
using V6Soft.Models.Accounting.ViewModels.DonViCoSo;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/donvicosos")]
    public class BranchUnitController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IBranchUnitDataDealer m_Dealer;

        #endregion

        #region Construtor
        public BranchUnitController(IBranchUnitDataDealer dealer)
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
        public PagedSearchResult<BranchUnitListItem> GetBranchUnits(SearchCriteria criteria)
        {
            PagedSearchResult<BranchUnitListItem> result = m_Dealer.GetBranchUnits(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddBranchUnit(AccModels.BranchUnit donvicoso)
        {
            donvicoso.UID = NextUID;
            var result = m_Dealer.AddBranchUnit(donvicoso);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteBranchUnit(string key)
        {
            m_Dealer.DeleteBranchUnit(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateBranchUnit(AccModels.BranchUnit donvicoso)
        {
            m_Dealer.UpdateBranchUnit(donvicoso);
            return Ok();
        }
        #endregion
    }
}