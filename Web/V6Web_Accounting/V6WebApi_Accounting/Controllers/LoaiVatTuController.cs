using System.Net;
using System.Web.Http;
using V6Soft.Accounting.MaterialType.Dealers;
using V6Soft.Models.Accounting.ViewModels.MaterialType;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/loaivattus")]
    public class MaterialTypeController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IMaterialTypeDataDealer m_Dealer;

        #endregion

        #region Construtor
        public MaterialTypeController(IMaterialTypeDataDealer dealer)
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
        public PagedSearchResult<MaterialTypeItem> GetMaterialTypes(SearchCriteria criteria)
        {
            PagedSearchResult<MaterialTypeItem> result = m_Dealer.GetMaterialTypes(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddMaterialType(AccModels.MaterialType loaivattu)
        {
            loaivattu.UID = NextUID;
            var result = m_Dealer.AddMaterialType(loaivattu);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteMaterialType(string key)
        {
            m_Dealer.DeleteMaterialType(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateMaterialType(AccModels.MaterialType loaivattu)
        {
            m_Dealer.UpdateMaterialType(loaivattu);
            return Ok();
        }
        #endregion
    }
}