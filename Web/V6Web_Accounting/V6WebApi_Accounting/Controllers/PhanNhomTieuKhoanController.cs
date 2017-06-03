using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Banking.Dealers;
using V6Soft.Models.Accounting.ViewModels.PhanNhomTieuKhoan;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/phannhomtieukhoans")]
    public class PhanNhomTieuKhoanController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IPhanNhomTieuKhoanDataDealer m_Dealer;

        #endregion

        #region Construtor
        public PhanNhomTieuKhoanController(IPhanNhomTieuKhoanDataDealer dealer)
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
        public PagedSearchResult<PhanNhomTieuKhoanItem> GetPhanNhomTieuKhoans(SearchCriteria criteria)
        {
            PagedSearchResult<PhanNhomTieuKhoanItem> result = m_Dealer.GetPhanNhomTieuKhoans(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddPhanNhomTieuKhoan(AccModels.PhanNhomTieuKhoan phannhomtieukhoan)
        {
            phannhomtieukhoan.UID = NextUID;
            var result = m_Dealer.AddPhanNhomTieuKhoan(phannhomtieukhoan);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeletePhanNhomTieuKhoan(string key)
        {
            m_Dealer.DeletePhanNhomTieuKhoan(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdatePhanNhomTieuKhoan(AccModels.PhanNhomTieuKhoan phannhomtieukhoan)
        {
            m_Dealer.UpdatePhanNhomTieuKhoan(phannhomtieukhoan);
            return Ok();
        }
        #endregion
    }
} 