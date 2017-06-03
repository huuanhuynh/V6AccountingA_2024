using System.Net;
using System.Web.Http;
using V6Soft.Accounting.MaterialPriceGroup.Dealers;
using V6Soft.Models.Accounting.ViewModels.MaterialPriceGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/nhomgiavattus")]
    public class MaterialPriceGroupController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IMaterialPriceGroupDataDealer m_Dealer;

        #endregion

        #region Construtor
        public MaterialPriceGroupController(IMaterialPriceGroupDataDealer dealer)
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
        public PagedSearchResult<MaterialPriceGroupListItem> GetMaterialPriceGroups(SearchCriteria criteria)
        {
            PagedSearchResult<MaterialPriceGroupListItem> result = m_Dealer.GetMaterialPriceGroups(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddMaterialPriceGroup(AccModels.MaterialPriceGroup nhomgiavattu)
        {
            nhomgiavattu.UID = NextUID;
            var result = m_Dealer.AddMaterialPriceGroup(nhomgiavattu);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteMaterialPriceGroup(string key)
        {
            m_Dealer.DeleteMaterialPriceGroup(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateMaterialPriceGroup(AccModels.MaterialPriceGroup nhomgiavattu)
        {
            m_Dealer.UpdateMaterialPriceGroup(nhomgiavattu);
            return Ok();
        }
        #endregion
    }
}