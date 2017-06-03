using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Account.Dealers;
using V6Soft.Accounting.Banking.Dealers;
using V6Soft.Models.Accounting.ViewModels.Account;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/taikhoans")]
    public class AccountController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IAccountDataDealer m_Dealer;

        #endregion

        #region Construtor
        public AccountController(IAccountDataDealer dealer)
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
        public PagedSearchResult<AccountListItem> GetAccounts(SearchCriteria criteria)
        {
            PagedSearchResult<AccountListItem> result = m_Dealer.GetAccounts(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddAccount(AccModels.TaiKhoan taikhoan)
        {
            taikhoan.UID = NextUID;
            var result = m_Dealer.AddAccount(taikhoan);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteAccount(string key)
        {
            m_Dealer.DeleteAccount(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateAccount(AccModels.TaiKhoan taikhoan)
        {
            m_Dealer.UpdateAccount(taikhoan);
            return Ok();
        }
        #endregion
    }
}