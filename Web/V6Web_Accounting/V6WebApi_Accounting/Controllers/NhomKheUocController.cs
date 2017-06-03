using System.Net;
using System.Web.Http;
using V6Soft.Accounting.IndentureGroup.Dealers;
using V6Soft.Models.Accounting.ViewModels.IndentureGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/nhomkheuocs")]
    public class IndentureGroupController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IIndentureGroupDataDealer m_Dealer;

        #endregion

        #region Construtor
        public IndentureGroupController(IIndentureGroupDataDealer dealer)
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
        public PagedSearchResult<IndentureGroupListItem> GetIndentureGroups(SearchCriteria criteria)
        {
            PagedSearchResult<IndentureGroupListItem> result = m_Dealer.GetIndentureGroups(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddIndentureGroup(AccModels.IndentureGroup nhomkheuoc)
        {
            nhomkheuoc.UID = NextUID;
            var result = m_Dealer.AddIndentureGroup(nhomkheuoc);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteIndentureGroup(string key)
        {
            m_Dealer.DeleteIndentureGroup(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateIndentureGroup(AccModels.IndentureGroup nhomkheuoc)
        {
            m_Dealer.UpdateIndentureGroup(nhomkheuoc);
            return Ok();
        }
        #endregion
    }
}