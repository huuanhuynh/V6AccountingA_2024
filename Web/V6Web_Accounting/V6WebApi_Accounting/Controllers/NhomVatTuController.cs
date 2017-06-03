using System.Net;
using System.Web.Http;
using V6Soft.Accounting.MaterialGroup.Dealers;
using V6Soft.Models.Accounting.ViewModels.MaterialGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/nhomvattus")]
    public class MaterialGroupController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IMaterialGroupDataDealer m_Dealer;

        #endregion

        #region Construtor
        public MaterialGroupController(IMaterialGroupDataDealer dealer)
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
        public PagedSearchResult<MaterialGroupListItem> GetMaterialGroups(SearchCriteria criteria)
        {
            PagedSearchResult<MaterialGroupListItem> result = m_Dealer.GetMaterialGroups(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddMaterialGroup(AccModels.MaterialGroup nhomvattu)
        {
            nhomvattu.UID = NextUID;
            var result = m_Dealer.AddMaterialGroup(nhomvattu);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteMaterialGroup(string key)
        {
            m_Dealer.DeleteMaterialGroup(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateMaterialGroup(AccModels.MaterialGroup nhomvattu)
        {
            m_Dealer.UpdateMaterialGroup(nhomvattu);
            return Ok();
        }
        #endregion
    }
}