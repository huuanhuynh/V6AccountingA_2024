using System.Net;
using System.Web.Http;
using V6Soft.Accounting.AccountType.Dealers;
using V6Soft.Models.Accounting.ViewModels.AccountType;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/phanloaitaikhoans")]
    public class PhanLoaiTaiKhoanController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IAccountTypeDataDealer m_Dealer;

        #endregion

        #region Construtor
        public PhanLoaiTaiKhoanController(IAccountTypeDataDealer dealer)
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
        public PagedSearchResult<AccountTypeListItem> GetPhanLoaiTaiKhoans(SearchCriteria criteria)
        {
            PagedSearchResult<AccountTypeListItem> result = m_Dealer.GetAccountTypes(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddPhanLoaiTaiKhoan(Models.Accounting.DTO.AccountType phanloaitaikhoan)
        {
            phanloaitaikhoan.UID = NextUID;
            var result = m_Dealer.AddAccountType(phanloaitaikhoan);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteDepartment(string key)
        {
            m_Dealer.DeleteAccountType(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateDepartment(AccModels.AccountType phanloaitaikhoan)
        {
            m_Dealer.UpdateAccountType(phanloaitaikhoan);
            return Ok();
        }
        #endregion
    }
}