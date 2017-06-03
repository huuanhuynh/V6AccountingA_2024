using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Material.Dealers;
using V6Soft.Models.Accounting.ViewModels.Material;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/vattus")]
    public class MaterialController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IMaterialDataDealer m_Dealer;

        #endregion

        #region Construtor
        public MaterialController(IMaterialDataDealer dealer)
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
        public PagedSearchResult<MaterialListItem> GetMaterials(SearchCriteria criteria)
        {
            PagedSearchResult<MaterialListItem> result = m_Dealer.GetMaterials(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddMaterial(AccModels.Material vattu)
        {
            vattu.UID = NextUID;
            var result = m_Dealer.AddMaterial(vattu);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteMaterial(string key)
        {
            m_Dealer.DeleteMaterial(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateMaterial(AccModels.Material vattu)
        {
            m_Dealer.UpdateMaterial(vattu);
            return Ok();
        }
        #endregion
    }
}