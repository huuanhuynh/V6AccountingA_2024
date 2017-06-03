using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Asset.Dealers;
using V6Soft.Accounting.Department.Dealers;
using V6Soft.Accounting.PhanNhomCongCu.Dealers;
using V6Soft.Models.Accounting.ViewModels.Department;
using V6Soft.Models.Accounting.ViewModels.PhanNhomCongCu;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/phannhomcongcus")]
    public class PhanNhomCongCuController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IPhanNhomCongCuDataDealer m_Dealer;

        #endregion

        #region Construtor
        public PhanNhomCongCuController(IPhanNhomCongCuDataDealer dealer)
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
        public PagedSearchResult<PhanNhomCongCuItem> GetPhanNhomCongCus(SearchCriteria criteria)
        {
            PagedSearchResult<PhanNhomCongCuItem> result = m_Dealer.GetPhanNhomCongCus(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddPhanNhomCongCu(Models.Accounting.DTO.PhanNhomCongCu phannhomcongcu)
        {
            phannhomcongcu.UID = NextUID;
            var result = m_Dealer.AddPhanNhomCongCu(phannhomcongcu);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteDepartment(string key)
        {
            m_Dealer.DeletePhanNhomCongCu(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateDepartment(AccModels.PhanNhomCongCu phannhomcongcu)
        {
            m_Dealer.UpdatePhanNhomCongCu(phannhomcongcu);
            return Ok();
        }
        #endregion
    }
}