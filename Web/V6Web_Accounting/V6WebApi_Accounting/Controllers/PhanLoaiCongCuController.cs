using System.Net;
using System.Web.Http;
using V6Soft.Accounting.EquipmentType.Dealers;
using V6Soft.Models.Accounting.ViewModels.EquipmentType;
using V6Soft.Models.Accounting.ViewModels.PhanLoaiCongCu;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/phanloaicongcus")]
    public class PhanLoaiCongCuController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IEquipmentTypeDataDealer m_Dealer;

        #endregion

        #region Construtor
        public PhanLoaiCongCuController(IEquipmentTypeDataDealer dealer)
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
        public PagedSearchResult<EquipmentTypeListItem> GetPhanLoaiCongCus(SearchCriteria criteria)
        {
            PagedSearchResult<EquipmentTypeListItem> result = m_Dealer.GetEquipmentTypes(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddPhanLoaiCongCu(AccModels.PhanLoaiCongCu phanloaicongcu)
        {
            phanloaicongcu.UID = NextUID;
            var result = m_Dealer.AddEquipmentType(phanloaicongcu);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeletePhanLoaiCongCu(string key)
        {
            m_Dealer.DeleteEquipmentType(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdatePhanLoaiCongCu(AccModels.PhanLoaiCongCu phanloaicongcu)
        {
            m_Dealer.UpdateEquipmentType(phanloaicongcu);
            return Ok();
        }
        #endregion
    }
}