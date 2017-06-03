using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Banking.Dealers;
using V6Soft.Models.Accounting.ViewModels.BankAccount;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/taikhoannganhangs")]
    public class BankAccountController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IBankAccountDataDealer m_Dealer;

        #endregion

        #region Construtor
        public BankAccountController(IBankAccountDataDealer dealer)
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
        public PagedSearchResult<BankAccountListItem> GetBankAccounts(SearchCriteria criteria)
        {
            PagedSearchResult<BankAccountListItem> result = m_Dealer.GetBankAccounts(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddBankAccount(AccModels.BankAccount taikhoannganhang)
        {
            taikhoannganhang.UID = NextUID;
            var result = m_Dealer.AddBankAccount(taikhoannganhang);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteBankAccount(string key)
        {
            m_Dealer.DeleteBankAccount(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateBankAccount(AccModels.BankAccount taikhoannganhang)
        {
            m_Dealer.UpdateBankAccount(taikhoannganhang);
            return Ok();
        }
        #endregion
    }
}